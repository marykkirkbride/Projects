namespace Events.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Events.Data;
    using Events.Domain;
    using Events.VModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    [Produces("application/json")]
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly EventContext _eventContext;
        private readonly IOptionsSnapshot<EventSettings> _settings;

        private List<Event> ChangeUrlPlaceHolder(List<Event> items)
        {
            items.ForEach(
                x => x.PictureUrl =
                x.PictureUrl
                .Replace("http://externaleventbaseurltobereplaced",
                _settings.Value.ExternalEventBaseUrl));
            return items;
        }

        public EventController(EventContext eventContext, IOptionsSnapshot<EventSettings> settings)
        {
            _eventContext = eventContext;
            _settings = settings;
        }

        //GET APIs
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllEvents()
        {
            var allEvents = await _eventContext.Events.ToListAsync();
            return Ok(allEvents);
        }

        [HttpGet]
        [Route("[action]/{eventId:int}")]
        public async Task<IActionResult> GetEventByEventId(int eventId)
        {
            if (eventId <= 0)
            {
                return BadRequest();
            }
            var item = await _eventContext.Events
                .SingleOrDefaultAsync(e => e.EventId == eventId);
            if (item != null)
            {
                item.PictureUrl = item.PictureUrl.Replace("http://externaleventbaseurltobereplaced",
                _settings.Value.ExternalEventBaseUrl);
                return Ok(item);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Events(
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventContext.Events
                              .LongCountAsync();
            var itemsOnPage = await _eventContext.Events
                              .OrderBy(c => c.EventName)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

            var model = new PaginatedEventsViewModel<Event>
                (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //GET api/Catalog/items/withname/Wonder?pageSize=2&pageIndex=0
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        public async Task<IActionResult> Events(string name,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventContext.Events
                               .Where(c => c.EventName.StartsWith(name))
                              .LongCountAsync();
            var itemsOnPage = await _eventContext.Events
                              .Where(c => c.EventName.StartsWith(name))
                              .OrderBy(c => c.EventName)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventsViewModel<Event>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        // GET api/Catalog/Items/type/1/brand/null[?pageSize=4&pageIndex=0]
        [HttpGet]
        [Route("[action]/eventType/{eventTypeId}/venue/{venueId}")]
        public async Task<IActionResult> Events(int? eventTypeId,
            int? venueId,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<Event>)_eventContext.Events;

            if (eventTypeId.HasValue)
            {
                root = root.Where(c => c.EventTypeId == eventTypeId);
            }
            if (venueId.HasValue)
            {
                root = root.Where(c => c.VenueId == venueId);
            }

            var totalItems = await root
                              .LongCountAsync();
            var itemsOnPage = await root

                              .OrderBy(c => c.EventName)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventsViewModel<Event>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }


        [HttpGet]
        [Route("[action]/{eventId:int}")]
        public async Task<IActionResult> GetTicketsSoldCount(int eventId)
        {
            if (eventId <= 0)
            {
                return BadRequest();
            }
            var ticketsSold = await _eventContext.UserEvents
                .Where(ue => ue.EventId == eventId)
                .CountAsync();
            return Ok(ticketsSold);
        }

        //DELETE APIs
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var _event = await _eventContext.Events
                .SingleOrDefaultAsync(v => v.EventId == eventId);
            if (_event == null)
            {
                return NotFound();
            }

            var _usereventEntries = _eventContext.UserEvents.Where(ue => ue.EventId == eventId);
            foreach (var item in _usereventEntries)
            {
                _eventContext.UserEvents.Remove(item);
            }
            _eventContext.Events.Remove(_event);
            await _eventContext.SaveChangesAsync();
            return NoContent();
        }

        //PUT APIs
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEvent([FromBody] Event eventToUpdate)
        {
            var _event = await _eventContext.Events
                              .SingleOrDefaultAsync
                              (i => i.EventId == eventToUpdate.EventId);
            if (_event == null)
            {
                return NotFound(new { Message = $"Environment with id {eventToUpdate.EventId} not found." });
            }
            _event = eventToUpdate;
            _eventContext.Events.Update(_event);
            await _eventContext.SaveChangesAsync();
            return await GetEventByEventId(eventToUpdate.EventId);
        }

        //POST APIs
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateEvent([FromBody] Event _event)
        {
            var eventEntry = new Event
            {
                EventName = _event.EventName,
                Cost = _event.Cost,
                DateTime = _event.DateTime,
                Description = _event.Description,
                PictureUrl = _event.PictureUrl,
                EventTypeId = _event.EventTypeId,
                HostId = _event.HostId,
                VenueId = _event.VenueId,
            };
            _eventContext.Events.Add(eventEntry);
            await _eventContext.SaveChangesAsync();
            return await GetEventByEventId(eventEntry.EventId);
        }
    }
}