using System.ComponentModel.DataAnnotations;

namespace Bloodhound.Core.Model
{
    public class EventType
    {
        public int EventTypeId { get; set; }

        [Required, MaxLength(128), Display(Name = "Event Type")]
        public string EventTypeName { get; set; }
    }
}
