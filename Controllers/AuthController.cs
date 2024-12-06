using api.Data;
using api.Models;
using api.Models.DTO;
using api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly JwtTokenService _jwtTokenService;
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context, JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser dto)
        {
            // Check if the username already exists
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Generate the password hash and salt
            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hash,
                Salt = salt,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            // Save the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            // Find the user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

            // If user doesn't exist or password doesn't match, return Unauthorized
            if (user == null || !PasswordHasher.VerifyPassword(login.Password, user.PasswordHash, user.Salt))
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate JWT token for the authenticated user
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

    }
}