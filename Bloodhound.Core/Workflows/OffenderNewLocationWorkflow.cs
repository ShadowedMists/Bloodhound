using Bloodhound.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloodhound.Core.Workflows
{
    public class OffenderNewLocationWorkflow
    {
        protected BloodhoundContext dbContext;
        protected Offender offender;
        protected OffenderLocation lastLocation;
        protected List<OffenderGeoFence> geoFences;

        public OffenderNewLocationWorkflow(BloodhoundContext dbContext, long offenderId)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(offenderId));
            this.offender = dbContext.Offenders.Find(offenderId) ?? throw new EntityNotFoundException(nameof(Offender), offenderId);
            this.lastLocation = dbContext.OffenderLocations.Where(x => x.OffenderId == this.offender.OffenderId).Take(1).OrderByDescending(x => x.LocationTime).FirstOrDefault();
            this.geoFences = dbContext.OffenderGeoFences.Where(x => x.OffenderId == this.offender.OffenderId).ToList();
        }

        public void AddNewLocation(decimal latitude, decimal longitude, DateTimeOffset locationTime)
        {
            
        }
    }
}
