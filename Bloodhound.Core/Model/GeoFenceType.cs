using System.ComponentModel.DataAnnotations;

namespace Bloodhound.Core.Model
{
    public class GeoFenceType
    {
        public int GeoFenceTypeId { get; set; }

        [Required, MaxLength(128), Display(Name = "GeoFence Type ")]
        public string GeoFenceTypeName { get; set; }
    }
}
