using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebMvc.Models
{
    public class EventCatalog
    {
        public int PageIndex { get; set; }
        public int PageSize {get; set;}
        public int Count { get; set; }
        
        public List<EventCatalogItem> Data { get; set; }
    }
}
