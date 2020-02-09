using System.ComponentModel.DataAnnotations;

namespace Bloodhound.Core.Model
{
    public class Offender
    {
        public long OffenderId { get; set; }

        [Required, MaxLength(128), Display(Name = "Offender Name")]
        public string OffenderName { get; set; }

        [Required, MaxLength(2048), Display(Name = "Summary")]
        public string OffenderSummary { get; set; }
    }
}
