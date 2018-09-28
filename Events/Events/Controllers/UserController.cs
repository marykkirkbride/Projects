namespace Events.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Events.Data;
    using Events.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly EventContext _eventContext;

        public UserController(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        //GET APIs
        [HttpGet]
        [Route("[action]/{userId:int}")]
        public async Task<IActionResult> GetUserbyId(int userId)
        {
            if (userId <=0)
            {
                return BadRequest();
            }

            var user = await _eventContext.Users
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if(user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        //POST APIs
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                EmailAddress = user.EmailAddress
            };
            _eventContext.Users.Add(newUser);
            await _eventContext.SaveChangesAsync();
            return await GetUserbyId(newUser.UserId);
        }

        //PUT APIs
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] User userToUpdate)
        {
            var user = await _eventContext.Users
                .SingleOrDefaultAsync(u => u.UserId == userToUpdate.UserId);
            if(user == null)
            {
                return NotFound(new { Message = $"User with id {userToUpdate.UserId} not found." });
            }
            user = userToUpdate;
            _eventContext.Users.Update(user);
            await _eventContext.SaveChangesAsync();
            return await GetUserbyId(user.UserId);
        }

        //DELETE APIs
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int userid)
        {
            var _user = await _eventContext.Users.SingleOrDefaultAsync(u => u.UserId == userid);
            if (_user == null)
            {
                return NotFound();
            }
            var _userevent = _eventContext.UserEvents.Where(ue => ue.GuestId == userid); 
               foreach(var user in _userevent)
            {
                _eventContext.UserEvents.Remove(user);
            }
            _eventContext.Users.Remove(_user);
            await _eventContext.SaveChangesAsync();
            return NoContent();
        }
    }
}