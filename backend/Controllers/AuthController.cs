using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetFullStack.API.Data;
using NetFullStack.API.Models;

namespace NetFullStack.API.Controllers
{
    /// <summary>
    /// Controller providing a simple authentication endpoint that returns a JWT token.
    /// This implementation is intentionally lightweight to illustrate the pattern and
    /// should not be used as‑is in production.  See README.md for guidance on
    /// extending authentication and authorization in a real application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Login endpoint that accepts a username (and optional email) and returns
        /// a JWT token if the user exists.  In a real application you would also
        /// verify passwords and possibly email confirmation status.
        /// </summary>
        /// <param name="request">Login request containing a username and optional email.</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest("Username is required");
            }

            // Find the user by name or email.  This example uses only Name
            // because the seeded data does not include passwords.
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == request.Username || u.Email == request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:Key"] ?? "SuperSecretKey@345";
            var key = Encoding.UTF8.GetBytes(secretKey);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }

        /// <summary>
        /// Simple DTO for login requests.  In production you might add
        /// validation attributes and a password field.
        /// </summary>
        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string? Email { get; set; }
        }
    }
}