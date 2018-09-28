using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.web.EventsWebMvc.Infrastructure
{
    public class ApiPaths
    {

        public static class EventCatalog
        {
            // Get all events.
            public static string GetAllEventItems(string baseUri, int page, int take, int? venue, int? eventType)
            {
                var filterQs = "";
                if (venue.HasValue || eventType.HasValue)
                {
                    var venueQs = (venue.HasValue) ? venue.Value.ToString() : "null";
                    var eventTypeQs = (eventType.HasValue) ? eventType.Value.ToString() : "null";
                    filterQs = $"/eventType/{eventTypeQs}/venue/{venueQs}";
                }

                return $"{baseUri}Events{filterQs}?pageIndex={page}&pageSize={take}";
            }



            // Get single event.
            public static string GetAllVenues(string baseUri, int eventId)
            {
                return $"{baseUri}/event/GetEventByEventId/{eventId}";
            }

            // Get all venues? (Like get all brand for shoes?)
            public static string GetAllVenues(string baseUri)
            {
                return $"{baseUri}GetAllVenues";
            }

            // Get all Event Types. Do we still need to make this? 
            public static string GetAllEventTypes(string baseUri)
            {
                return $"{baseUri}GetAllEventTypes";
            }
        }
    }
}
