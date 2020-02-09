using Bloodhound.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using Bloodhound.Core.Workflows;
using System.Linq;

namespace Bloodhound.Core.Tests
{
    [TestClass]
    public class OffenderNewLocationWorkflowTests
    {
        protected BloodhoundContext dbContext;

        [TestInitialize]
        public void Initialize()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DbContextOptionsBuilder<BloodhoundContext> optionsBuilder = new DbContextOptionsBuilder<BloodhoundContext>();
            optionsBuilder.UseSqlServer(configurationBuilder.GetConnectionString("BloodhoundContext"));
            this.dbContext = new BloodhoundContext(optionsBuilder.Options);
            this.dbContext.Database.EnsureCreated();
            this.dbContext.Database.Migrate();
        }

        [TestMethod]
        public void TestOffenderEntry()
        {
            // retreive the first offender
            Offender offender = this.dbContext.Offenders.Find(1L);
            Assert.IsNotNull(offender);

            // retreive the offender's geofence
            OffenderGeoFence geoFence = this.dbContext.OffenderGeoFences.FirstOrDefault(x => x.OffenderId == offender.OffenderId);
            Assert.IsNotNull(geoFence);

            // set the last location to outside their geofence
            this.dbContext.OffenderLocations.Add(new OffenderLocation()
            {
                OffenderId = offender.OffenderId,
                Latitude = geoFence.SouthWestLatitude + .0123M,
                Longitude = (geoFence.NorthEastLongitude + geoFence.SouthWestLongitude) / 2,
                LocationTime = DateTimeOffset.Now.AddMinutes(-1)
            });
            this.dbContext.SaveChanges();

            // the location inside the fence
            decimal latitude = (geoFence.NorthEastLatitude + geoFence.SouthWestLatitude) / 2;
            decimal longitude = (geoFence.NorthEastLongitude + geoFence.SouthWestLongitude) / 2;
            DateTimeOffset locationTime = DateTimeOffset.Now;

            // add the location to the offender
            OffenderNewLocationWorkflow workflow = new OffenderNewLocationWorkflow(this.dbContext, offender.OffenderId);
            workflow.AddNewLocation(latitude, longitude, locationTime);

            // ensure the location was saved to the database
            OffenderLocation insideLocation = this.dbContext.OffenderLocations.Where(x => x.OffenderId == offender.OffenderId).OrderByDescending(x => x.LocationTime).Take(1).FirstOrDefault();
            Assert.IsNotNull(insideLocation, "Expected Offender to have a last location");
            Assert.AreEqual(latitude, insideLocation.Latitude, "Expected the last locations Latidude to equal {0}", latitude);
            Assert.AreEqual(longitude, insideLocation.Longitude, "Expected the last locations Longitude to equal {0}", longitude);
            Assert.AreEqual(locationTime, insideLocation.LocationTime, "Expected the last locations LocationTime to equal {0}", locationTime);

            // validate the event
            OffenderEvent entryEvent = this.dbContext.OffenderEvents.FirstOrDefault(x => x.OffenderLocationId == insideLocation.OffenderLocationId);
            Assert.IsNotNull(entryEvent, "No Offender Event was created for the Location.");
            Assert.AreEqual(geoFence.OffenderGeoFenceId, entryEvent.OffenderGeoFenceId, "Offender Event does not match the expected GeoFence");
            Assert.AreEqual(offender.OffenderId, entryEvent.OffenderId, "The Offender Event does not match the expected Offender");
            Assert.AreEqual(EventTypeIdentifiers.Entry, entryEvent.EventTypeId, "The Event Type was expected to be an Entry event");
        }

        [TestMethod]
        public void TestOffenderExit()
        {
            // retreive the second offender
            Offender offender = this.dbContext.Offenders.Find(2L);
            Assert.IsNotNull(offender);

            // retreive the offender's geofence
            OffenderGeoFence geoFence = this.dbContext.OffenderGeoFences.FirstOrDefault(x => x.OffenderId == offender.OffenderId);
            Assert.IsNotNull(geoFence);

            // set the last location to inside their geofence
            this.dbContext.OffenderLocations.Add(new OffenderLocation()
            {
                OffenderId = offender.OffenderId,
                Latitude = (geoFence.NorthEastLatitude + geoFence.SouthWestLatitude) / 2,
                Longitude = (geoFence.NorthEastLongitude + geoFence.SouthWestLongitude) / 2,
                LocationTime = DateTimeOffset.Now.AddMinutes(-1)
            });
            this.dbContext.SaveChanges();

            // the location outside the fence
            decimal latitude = geoFence.SouthWestLatitude + .0123M;
            decimal longitude = (geoFence.NorthEastLongitude + geoFence.SouthWestLongitude) / 2;
            DateTimeOffset locationTime = DateTimeOffset.Now;

            // add the location to the offender
            OffenderNewLocationWorkflow workflow = new OffenderNewLocationWorkflow(this.dbContext, offender.OffenderId);
            workflow.AddNewLocation(latitude, longitude, locationTime);

            // ensure the location was saved to the database
            OffenderLocation insideLocation = this.dbContext.OffenderLocations.Where(x => x.OffenderId == offender.OffenderId).OrderByDescending(x => x.LocationTime).Take(1).FirstOrDefault();
            Assert.IsNotNull(insideLocation, "Expected Offender to have a last location");
            Assert.AreEqual(latitude, insideLocation.Latitude, "Expected the last locations Latidude to equal {0}", latitude);
            Assert.AreEqual(longitude, insideLocation.Longitude, "Expected the last locations Longitude to equal {0}", longitude);
            Assert.AreEqual(locationTime, insideLocation.LocationTime, "Expected the last locations LocationTime to equal {0}", locationTime);

            // validate the event
            OffenderEvent entryEvent = this.dbContext.OffenderEvents.FirstOrDefault(x => x.OffenderLocationId == insideLocation.OffenderLocationId);
            Assert.IsNotNull(entryEvent, "No Offender Event was created for the Location.");
            Assert.AreEqual(geoFence.OffenderGeoFenceId, entryEvent.OffenderGeoFenceId, "Offender Event does not match the expected GeoFence");
            Assert.AreEqual(offender.OffenderId, entryEvent.OffenderId, "The Offender Event does not match the expected Offender");
            Assert.AreEqual(EventTypeIdentifiers.Exit, entryEvent.EventTypeId, "The Event Type was expected to be an Exit event");
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.dbContext?.Dispose();
            this.dbContext = null;
        }
    }
}
