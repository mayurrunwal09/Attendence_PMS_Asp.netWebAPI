using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositroy_And_Services.context;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly MainDBContext _context;

        public SessionsController(MainDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllSessions")]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Sessions>>> GetAllSessions()
        {
            var sessions = await _context.Sessions
                .Include(s => s.User)
                .ToListAsync();

            var sessionsWithUsername = sessions.Select(s => new
            {
                Id = s.Id,
                EventName = s.EventName,
                EventType = s.EventType,
                EventDate = s.EventDate,
                UserId = s.UserId,
                Username = s.User.UserName,
                MentorName = s.MentorName
            });

            return Ok(sessionsWithUsername);
        }

        [HttpGet("{id}")]
       // [Authorize]
        public async Task<ActionResult<Sessions>> GetSession(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        [HttpPost("PostSession")]
       //  [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Sessions>> PostSession(EventInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var session = new Sessions
            {
                EventName = model.EventName,
                EventType = model.EventType,
                EventDate = model.EventDateTime, // Assuming EventDateTime should be used for EventDate
                MentorName = model.MentorName,
                UserId = user.Id
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.Id }, session);
        }

        // Other actions (Update, Delete, GetUserSessions) can be implemented similarly

        public class EventInputModel
        {
            public string EventName { get; set; }
            public string EventType { get; set; }
            public DateTime EventDateTime { get; set; }
            public string Username { get; set; }
            public string MentorName { get; set; }
        }
        [HttpGet("GetUserSessions")]
        // [Authorize]
        public async Task<ActionResult<IEnumerable<object>>> GetUserSessions(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userSessions = user.Sessions.Select(s => new
            {
                Id = s.Id,
                EventName = s.EventName,
                EventType = s.EventType,
                EventDate = s.EventDate,
                UserId = s.UserId,
                Username = user.UserName, // Include the username here
                MentorName = s.MentorName
            });

            return Ok(userSessions);
        }



    }
}
