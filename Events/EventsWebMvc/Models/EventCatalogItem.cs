using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebMvc.Models
{
    public class EventCatalogItem
    {
        public string EventId { get; set; }
        public string HostId { get; set;  }
        public int EventTypeId { get; set; }
        public int VenueId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public float Cost { get; set; }
        public DateTime DateTime { get; set; }
    }
}
