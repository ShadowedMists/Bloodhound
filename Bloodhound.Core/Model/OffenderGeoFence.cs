using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bloodhound.Core.Model
{
    public class OffenderGeoFence
    {
        public long OffenderGeoFenceId { get; set; }

        [Required, MaxLength(128), Display(Name = "GeoFence Name")]
        public string GeoFenceName { get; set; }

        [Display(Name = "Offender")]
        public long OffenderId { get; set; }

        [Display(Name = "GeoFence Type")]
        public int GeoFenceTypeId { get; set; }

        [Required, MaxLength(256)]
        public string Address { get; set; }

        [Display(Name = "North East Latitude"), Column(TypeName = "decimal(12,6)")]
        public decimal NorthEastLatitude { get; set; }

        [Display(Name = "North East Longitude"), Column(TypeName = "decimal(12,6)")]
        public decimal NorthEastLongitude { get; set; }

        [Display(Name = "South West Latitude"), Column(TypeName = "decimal(12,6)")]
        public decimal SouthWestLatitude { get; set; }

        [Display(Name = "South West Longitude"), Column(TypeName = "decimal(12,6)")]
        public decimal SouthWestLongitude { get; set; }

        public Offender Offender { get; set; }
        public GeoFenceType GeoFenceType { get; set; }

        public bool IsInside(decimal latitude, decimal longitude)
        {
            return latitude <= this.NorthEastLatitude && latitude >= this.SouthWestLatitude &&
                longitude >= this.SouthWestLongitude && longitude <= this.NorthEastLongitude;
        }

        public bool IsInside(OffenderLocation location)
        {
            if (location == null)
                return false;

            return this.IsInside(location.Latitude, location.Longitude);
        }
    }
}
