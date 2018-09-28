namespace Events.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Events.Data;
    using Events.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Produces("application/json")]
    [Route("api/Venue")]
    public class VenueController : Controller
    {
        private readonly EventContext _venueContext;

        public VenueController(EventContext venueContext)
        {
            _venueContext = venueContext;
        }

        //GET APIs
        [HttpGet]
        [Route("[action]/{venueId:int}")]
        public async Task<IActionResult> GetMaxCapacityByVenueId(int venueId)
        {
            if (venueId <= 0)
            {
                return BadRequest();
            }
            var venue = await _venueContext.Venues
                .SingleOrDefaultAsync(c => c.VenueId == venueId);
            if (venue != null)
            {
                return Ok(venue.MaxCapacity);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{venueId:int}")]
        public async Task<IActionResult> GetVenueByVenueId(int venueId)
        {
            if (venueId <= 0)
            {
                return BadRequest();
            }
            var item = await _venueContext.Venues
                .SingleOrDefaultAsync(v => v.VenueId == venueId);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllVenues()
        {
            var allVenues = await _venueContext.Venues.ToListAsync();
            return Ok(allVenues);
        }

        //POST APIs
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateVenue([FromBody] Venue venue)
        {
            var venueEntry = new Venue
            {
                VenueName = venue.VenueName,
                Address = venue.Address,
                City = venue.City,
                MaxCapacity = venue.MaxCapacity,
                State = venue.State,
                ZipCode = venue.ZipCode
            };
            _venueContext.Venues.Add(venueEntry);
            await _venueContext.SaveChangesAsync();
            return await GetVenueByVenueId(venueEntry.VenueId);
        }

        //PUT APIs
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateVenue([FromBody] Venue venueToUpdate)
        {
            var venue = await _venueContext.Venues
                              .SingleOrDefaultAsync
                              (i => i.VenueId == venueToUpdate.VenueId);
            if (venue == null)
            {
                return NotFound(new { Message = $"Item with id {venueToUpdate.VenueId} not found." });
            }
            venue = venueToUpdate;
            _venueContext.Venues.Update(venue);
            await _venueContext.SaveChangesAsync();

            return await GetVenueByVenueId(venueToUpdate.VenueId);
        }

        //DELETE APIs
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteVenue(int venueId)
        {
            var venue = await _venueContext.Venues
                .SingleOrDefaultAsync(v => v.VenueId == venueId);
            if (venue == null)
            {
                return NotFound();
            }
            var _eventEntries = _venueContext.Events.Where(ue => ue.VenueId == venueId);
            foreach (var _eventEntry in _eventEntries)
            {
                var _usereventEntries = _venueContext.UserEvents.Where(ue => ue.EventId == _eventEntry.EventId);
                foreach (var _usereventEntry in _usereventEntries)
                {
                    _venueContext.UserEvents.Remove(_usereventEntry);
                }
                _venueContext.Events.Remove(_eventEntry);
            }
            _venueContext.Venues.Remove(venue);
            await _venueContext.SaveChangesAsync();
            return NoContent();
        }
    }
}