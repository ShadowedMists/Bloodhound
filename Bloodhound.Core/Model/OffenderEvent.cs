using System;
using System.ComponentModel.DataAnnotations;

namespace Bloodhound.Core.Model
{
    public class OffenderEvent
    {
        public long OffenderEventId { get; set; }

        [Display(Name = "Offender")]
        public long OffenderId { get; set; }

        [Display(Name = "Event Type")]
        public int EventTypeId { get; set; }

        [Display(Name = "Location")]
        public long? OffenderLocationId { get; set; }

        [Display(Name = "GeoFence")]
        public long? OffenderGeoFenceId { get; set; }

        [Display(Name = "Event Time")]
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;

        public Offender Offender { get; set; }
        public EventType EventType { get; set; }
        public OffenderLocation OffenderLocation { get; set; }
        public OffenderGeoFence OffenderGeoFence { get; set; }
    }
}
