using api.Data;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TimeCapsuleController : ControllerBase
    {
        //We will use this later
        // private readonly ITimeCapsuleRepository _repository;
        private readonly AppDbContext _context;

        public TimeCapsuleController(AppDbContext context)
        {
            _context = context;
        }

 [HttpPost]
        public async Task<IActionResult> CreateTimeCapsule([FromBody] CreateTimeCapsul dto)
        {
            var userId = GetUserIdFromToken(); // Get logged-in user's ID

            var capsule = new TimeCapsule
            {
                Title = dto.Title,
                Message = dto.Message,
                FileUrl = dto.FileUrl ?? "",
                ScheduledDelivery = dto.ScheduledDelivery,
                UserId = userId
            };

            _context.TimeCapsules.Add(capsule);
            await _context.SaveChangesAsync();

            return Ok("Time capsule created successfully.");
        }



        [HttpPost]
        public async Task<IActionResult> CreateTimeCapsule([FromBody] CreateTimeCapsul dto)
        {
            var userId = GetUserIdFromToken(); // Get logged-in user's ID

            var capsule = new TimeCapsule
            {
                Title = dto.Title,
                Message = dto.Message,
                FileUrl = dto.FileUrl ?? "",
                ScheduledDelivery = dto.ScheduledDelivery,
                UserId = userId
            };

            _context.TimeCapsules.Add(capsule);
            await _context.SaveChangesAsync();

            return Ok("Time capsule created successfully.");
        }


        [HttpGet]
        public async Task<IActionResult> GetTimeCapsules()
        {
            var userId = GetUserIdFromToken();
            // Get current time (UTC)
            var now = DateTime.UtcNow;
            var capsules = await _context.TimeCapsules
                                         .Where(tc => tc.UserId == userId)
                                         .Select(tc => new ReturnAllCapsules
                                         {
                                             Id = tc.Id,
                                             Title = tc.Title,
                                             ScheduledDelivery = tc.ScheduledDelivery,
                                             // If the scheduled delivery date has passed, show content, else null
                                             Message = tc.ScheduledDelivery <= now ? tc.Message : null,
                                             FileUrl = tc.ScheduledDelivery <= now ? tc.FileUrl : null,
                                             // Apply business logic to determine if the capsule is opened or locked
                                             Status = tc.ScheduledDelivery <= now ? "opened" : "locked"
                                         })
                                         .ToListAsync();

            return Ok(capsules);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeCapsule(int id)
        {
            var userId = GetUserIdFromToken();
            var capsule = await _context.TimeCapsules.FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == userId);

            if (capsule == null)
            {
                return NotFound("Time capsule not found.");
            }

            if (capsule.ScheduledDelivery > DateTime.UtcNow)
            {
                return StatusCode(403, "This time capsule is not ready to be opened yet.");
            }

            return Ok(capsule);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeCapsule(int id, [FromBody] TimeCapsule updatedCapsule)
        {
            var userId = GetUserIdFromToken();
            var capsule = await _context.TimeCapsules.FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == userId);

            if (capsule == null)
            {
                return NotFound("Time capsule not found.");
            }

            capsule.Title = updatedCapsule.Title;
            capsule.Message = updatedCapsule.Message;
            capsule.FileUrl = updatedCapsule.FileUrl;
            capsule.ScheduledDelivery = updatedCapsule.ScheduledDelivery;

            await _context.SaveChangesAsync();

            return Ok("Time capsule updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeCapsule(int id)
        {
            var userId = GetUserIdFromToken();
            var capsule = await _context.TimeCapsules.FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == userId);

            if (capsule == null)
            {
                return NotFound("Time capsule not found.");
            }

            _context.TimeCapsules.Remove(capsule);
            await _context.SaveChangesAsync();

            return Ok("Time capsule deleted successfully.");
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }

            return int.Parse(userIdClaim.Value);
        }

    }
}