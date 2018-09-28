using EventsWebMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsWebMvc.Services
{
    public interface IEventsService
    {   
        // Gets all events and 
        Task<EventCatalog> GetEventCatalogItems(int page, int take, int? venue, int? eventType);
        Task<IEnumerable<SelectListItem>> GetAllVenues();
        Task<IEnumerable<SelectListItem>> GetAllEventTypes();
    }
}

