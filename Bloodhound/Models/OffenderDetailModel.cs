using Bloodhound.Core;
using Bloodhound.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloodhound.Models
{
    public class OffenderDetailModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "route parameter")]
        public long id { get; set; }

        public Offender Offender { get; set; }

        public List<OffenderGeoFence> GeoFences { get; set; }

        public v_OffenderLastLocation LastLocation { get; set; }

        public void Initialize(BloodhoundContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            this.Offender = dbContext.Offenders.Find(id);
            if (this.Offender == null)
                throw new EntityNotFoundException(nameof(Offender), this.id);

            this.LastLocation = dbContext.v_OffenderLastLocation.FirstOrDefault(x => x.OffenderId == this.Offender.OffenderId);

            this.GeoFences = dbContext.OffenderGeoFences.Where(x => x.OffenderId == this.Offender.OffenderId).ToList();
        }

        public decimal GetMapCenterLatitude() => this.LastLocation != null ? this.LastLocation.Latitude : 42.472096M;
        public decimal GetMapCenterLongitude() => this.LastLocation != null ? this.LastLocation.Longitude : -93.691406M;
    }
}
