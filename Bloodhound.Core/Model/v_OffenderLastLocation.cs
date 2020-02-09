using System;
using System.ComponentModel.DataAnnotations;

namespace Bloodhound.Core.Model
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "name of database view")]
    public class v_OffenderLastLocation
    {
        [Key]
        public long OffenderId { get; set; }
        public string OffenderName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTimeOffset LocationTime { get; set; }
    }
}
