using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloodhound.Core.Model
{
    public class OffenderLocation
    {
        public long OffenderLocationId { get; set; }

        [Display(Name = "Offender")]
        public long OffenderId { get; set; }

        [Column(TypeName = "decimal(12,6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(12,6)")]
        public decimal Longitude { get; set; }

        [Display(Name = "Location Time")]
        public DateTimeOffset LocationTime { get; set; } = DateTimeOffset.Now;

        public Offender Offender { get; set; }
    }
}
