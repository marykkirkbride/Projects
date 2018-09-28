using Events.web.EventsWebMvc.Infrastructure;
using EventsWebMvc.Infrastructure;
using EventsWebMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsWebMvc.Services
{
    public class EventsService : IEventsService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        // private readonly ILogger<EventsService> _logger;
        private readonly string _remoteServiceEventBaseUrl;
        private readonly string _remoteServiceVenueBaseUrl;
        private readonly string _remoteServiceEventTypeBaseUrl;

        public EventsService(
            IOptionsSnapshot<AppSettings> settings,
            IHttpClient httpClient
            )
        {
            _settings = settings;
            _apiClient = httpClient;
            _remoteServiceEventBaseUrl = $"{_settings.Value.EventCatalogUrl}/api/event/";
            _remoteServiceVenueBaseUrl = $"{_settings.Value.EventCatalogUrl}/api/venue/";
            _remoteServiceEventTypeBaseUrl = $"{_settings.Value.EventCatalogUrl}/api/eventtype/";
        }

        public async Task<IEnumerable<SelectListItem>> GetAllEventTypes()
        {
            var getEventTypeUri = ApiPaths.EventCatalog.GetAllEventTypes(_remoteServiceEventTypeBaseUrl);
            var dataString = await _apiClient.GetStringAsync(getEventTypeUri);

            var _eventTypes = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            var eventTypes = JArray.Parse(dataString);

            foreach (var eventType in eventTypes.Children<JObject>())
            {
                _eventTypes.Add(new SelectListItem()
                {
                    Value = eventType.Value<string>("eventTypeId"),
                    Text = eventType.Value<string>("eventTypeName")
                });
            }
            return _eventTypes;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllVenues()
        {
            var getVenueUri = ApiPaths.EventCatalog.GetAllVenues(_remoteServiceVenueBaseUrl);
            var dataString = await _apiClient.GetStringAsync(getVenueUri);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            var venues = JArray.Parse(dataString);

            foreach (var venue in venues.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = venue.Value<string>("venueId"),
                    Text = venue.Value<string>("venueName")
                });
            }

            return items;
        }

        public async Task<EventCatalog> GetEventCatalogItems(int page, int take, int? venue, int? eventType)
        {
            var allEventItemsUri = ApiPaths.EventCatalog.GetAllEventItems(_remoteServiceEventBaseUrl, page, take, venue, eventType);
            var dataString = await _apiClient.GetStringAsync(allEventItemsUri);

            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);

            return response;
        }
    }
}
