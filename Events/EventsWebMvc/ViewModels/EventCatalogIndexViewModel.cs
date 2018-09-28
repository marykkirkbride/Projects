using EventsWebMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebMvc.ViewModels
{
    public class EventCatalogIndexViewModel
    {
        public IEnumerable<EventCatalogItem> EventCatalogItems { get; set; }
        public IEnumerable<SelectListItem> Venues { get; set; }
        public IEnumerable<SelectListItem> EventTypes { get; set; }
        public int? EventTypesFilterApplied { get; set; }
        public int? VenuesFilterApplied { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
