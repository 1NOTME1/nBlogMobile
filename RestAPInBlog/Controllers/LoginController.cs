using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestAPInBlog.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace RestAPInBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly nBlogDbContext _context;

        public LoginController(nBlogDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Hashowanie hasła podanego przez użytkownika
            var hashedPassword = HashPassword(loginRequest.Password);

            // Walidacja danych logowania
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                return Ok(new { Message = "Login successful", UserId = user.UserId });
            }

            return Unauthorized(new { Message = "Invalid email or password" });
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
