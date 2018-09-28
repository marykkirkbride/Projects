using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventsWebMvc.Models;
using EventsWebMvc.Services;
using EventsWebMvc.ViewModels;

namespace EventsWebMvc.Controllers
{
    public class EventCatalogController : Controller
    {
        private IEventsService _eventscatalogSvc;

        public EventCatalogController(IEventsService eventscatalogSvc) =>
            _eventscatalogSvc = eventscatalogSvc;

        public async Task<IActionResult> Index(int? EventTypesFilterApplied, int? VenuesFilterApplied, int? page)
        {

            int itemsPage = 10;
            var eventCatalog = await
                _eventscatalogSvc.GetEventCatalogItems
                (page ?? 0, itemsPage, EventTypesFilterApplied,
                VenuesFilterApplied);
            var vm = new EventCatalogIndexViewModel()
            {
                EventCatalogItems = eventCatalog.Data,
                Venues = await _eventscatalogSvc.GetAllVenues(),
                EventTypes = await _eventscatalogSvc.GetAllEventTypes(),
                VenuesFilterApplied = VenuesFilterApplied ?? 0,
                EventTypesFilterApplied = EventTypesFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPage, //catalog.Data.Count,
                    TotalItems = eventCatalog.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)eventCatalog.Count / itemsPage))
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
