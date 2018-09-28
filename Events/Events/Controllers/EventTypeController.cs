namespace Events.Controllers
{
    using System.Threading.Tasks;
    using Events.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Produces("application/json")]
    [Route("api/EventType")]
    public class EventTypeController : Controller
    {
        private readonly EventContext _eventTypeContext;

        public EventTypeController(EventContext eventTypeContext)
        {
            _eventTypeContext = eventTypeContext;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllEventTypes()
        {
            var allEventTypes = await _eventTypeContext.EventTypes.ToListAsync();
            return Ok(allEventTypes);
        }
    }
}
