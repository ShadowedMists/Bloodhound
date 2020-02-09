using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Bloodhound.Core.Model
{
    public class BloodhoundContext : DbContext
    {
        public BloodhoundContext() { }

        public BloodhoundContext(DbContextOptions<BloodhoundContext> options) : base(options) { }

        public DbSet<EventType> EventTypes { get; set; }

        public DbSet<Offender> Offenders { get; set; }

        public DbSet<OffenderGeoFence> OffenderGeoFences { get; set; }

        public DbSet<OffenderLocation> OffenderLocations { get; set; }

        public DbSet<OffenderEvent> OffenderEvents { get; set; }

        public DbSet<GeoFenceType> GeoFenceTypes { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "database naming convention")]
        public DbQuery<v_OffenderLastLocation> v_OffenderLastLocation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventType>().HasData(new EventType()
            {
                EventTypeId = EventTypeIdentifiers.Entry,
                EventTypeName = nameof(EventTypeIdentifiers.Entry)
            },
            new EventType()
            {
                EventTypeId = EventTypeIdentifiers.Exit,
                EventTypeName = nameof(EventTypeIdentifiers.Exit)
            },
            new EventType()
            {
                EventTypeId = EventTypeIdentifiers.Tamper,
                EventTypeName = nameof(EventTypeIdentifiers.Tamper)
            });

            modelBuilder.Entity<GeoFenceType>().HasData(new GeoFenceType()
            {
                GeoFenceTypeId = GeoFenceTypeIdentifiers.Inclusion,
                GeoFenceTypeName = nameof(GeoFenceTypeIdentifiers.Inclusion)
            },
            new GeoFenceType()
            {
                GeoFenceTypeId = GeoFenceTypeIdentifiers.Exclusion,
                GeoFenceTypeName = nameof(GeoFenceTypeIdentifiers.Exclusion)
            });

            modelBuilder.Entity<Offender>().HasData(new Offender()
            {
                OffenderId = 1,
                OffenderName = "Theodore Bundy",
                OffenderSummary = "Kidnapping, Murder (Ellensburg, WA)"
            },
            new Offender()
            {
                OffenderId = 2,
                OffenderName = "Jeffrey Dahmer",
                OffenderSummary = "Kidnapping, Murder, Cannibalism (Milwaukee, MN)"
            });

            modelBuilder.Entity<OffenderGeoFence>().HasData(new OffenderGeoFence()
            {
                OffenderGeoFenceId = 1,
                OffenderId = 1,
                GeoFenceName = "Central Washington University",
                GeoFenceTypeId = 2,
                Address = "400 E University Way, Ellensburg, WA 98926",
                NorthEastLatitude = 47.013964M,
                NorthEastLongitude = -120.531863M,
                SouthWestLatitude = 46.999670M,
                SouthWestLongitude = -120.548865M
            },
            new OffenderGeoFence()
            {
                OffenderGeoFenceId = 2,
                OffenderId = 2,
                GeoFenceName = "Columbia Correctional Institution",
                GeoFenceTypeId = 1,
                Address = "2925 Columbia Dr #127, Portage, WI 52901",
                NorthEastLatitude = 43.567909M,
                NorthEastLongitude = -89.486586M,
                SouthWestLatitude = 43.564084M,
                SouthWestLongitude = -89.493882M
            });

            modelBuilder.Entity<OffenderLocation>().HasData(new OffenderLocation()
            {
                OffenderLocationId = 1,
                OffenderId = 1,
                Latitude = 47.006817M,
                Longitude = -120.551M,
            }, new OffenderLocation()
            {
                OffenderLocationId = 2,
                OffenderId = 2,
                Latitude = 43.566M,
                Longitude = -89.490234M,
            });
        }

        public void p_Offender_Delete(long offenderId)
        {
            this.Database.ExecuteSqlCommand("p_Offender_Delete @p0", offenderId);
        }
        public void p_OffenderGeoFence_Delete(long offenderGeoFenceId)
        {
            this.Database.ExecuteSqlCommand("p_OffenderGeoFence_Delete @p0", offenderGeoFenceId);
        }
    }
}
