using api.Models;
using api.Models.DTO;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TimeCapsuleController : ControllerBase
    {
        private readonly ITimeCapsuleService _timeCapsuleService;

        public TimeCapsuleController(ITimeCapsuleService timeCapsuleService)
        {
            _timeCapsuleService = timeCapsuleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeCapsule([FromBody] CreateTimeCapsul dto)
        {
            var userId = GetUserIdFromToken(); // Get logged-in user's ID
            var capsule = await _timeCapsuleService.CreateTimeCapsuleAsync(userId, dto);
            return Ok("Time capsule created successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeCapsules()
        {
            var userId = GetUserIdFromToken();
            var capsules = await _timeCapsuleService.GetTimeCapsulesAsync(userId);
            return Ok(capsules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeCapsule(int id)
        {
            var userId = GetUserIdFromToken();
            var capsule = await _timeCapsuleService.GetTimeCapsuleAsync(userId, id);

            if (capsule == null)
            {
                return NotFound("Time capsule not found or not ready to open.");
            }

            return Ok(capsule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeCapsule(int id, [FromBody] UpdateCapsule updatedCapsule)
        {
            var userId = GetUserIdFromToken();
            var updated = await _timeCapsuleService.UpdateTimeCapsuleAsync(userId, id, updatedCapsule);

            if (!updated)
            {
                return NotFound("Time capsule not found.");
            }

            return Ok("Time capsule updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeCapsule(int id)
        {
            var userId = GetUserIdFromToken();
            var deleted = await _timeCapsuleService.DeleteTimeCapsuleAsync(userId, id);

            if (!deleted)
            {
                return NotFound("Time capsule not found.");
            }

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
