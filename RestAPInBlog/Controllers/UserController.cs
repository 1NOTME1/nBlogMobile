using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPInBlog.ForView;
using RestAPInBlog.Model;
using RestAPInBlog.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestAPInBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly nBlogDbContext _context;

        public UserController(nBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForView>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users
                .Include(u => u.Role) // Dołączamy dane z tabeli roles
                .Select(u => new UserForView
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    RegistrationDate = u.RegistrationDate,
                    RoleId = u.RoleId,
                    RoleName = u.Role.Name, // Pobieramy nazwę roli
                    PostCount = u.Posts.Count(),
                    CommentCount = u.Comments.Count(),
                    LikeCount = u.Likes.Count(),
                    Password = u.PasswordHash
                })
                .ToListAsync();

            return users;
        }


        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserForView>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users
                .Include(u => u.Role) // Dołączamy dane z tabeli roles
                .Include(u => u.Posts) // Dołączamy posty
                .Include(u => u.Comments) // Dołączamy komentarze
                .Include(u => u.Likes) // Dołączamy polubienia
                .Where(u => u.UserId == id)
                .Select(u => new UserForView
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    RegistrationDate = u.RegistrationDate,
                    RoleId = u.RoleId,
                    RoleName = u.Role.Name, // Pobieramy nazwę roli
                    PostCount = u.Posts.Count(), // Liczba postów
                    CommentCount = u.Comments.Count(), // Liczba komentarzy
                    LikeCount = u.Likes.Count(), // Liczba polubień
                    Password = u.PasswordHash
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserForView userView)
        {
            if (id != userView.UserId)
                return BadRequest();

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            existingUser.Username = userView.Username;
            existingUser.Email = userView.Email;
            existingUser.RegistrationDate = userView.RegistrationDate ?? existingUser.RegistrationDate;
            existingUser.RoleId = userView.RoleId;
            existingUser.PasswordHash = userView.Password;

            if (!string.IsNullOrWhiteSpace(userView.Password))
                existingUser.PasswordHash = HashPassword(userView.Password);

            _context.Entry(existingUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserForView>> PostUser(UserForView userView)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'nBlogDbContext.Users' is null.");
            }

            if (string.IsNullOrWhiteSpace(userView.Password))
            {
                return BadRequest("Password cannot be empty.");
            }

            // Przykładowe hashowanie hasła przed zapisem
            var hashedPassword = HashPassword(userView.Password);

            var user = new User
            {
                Username = userView.Username,
                Email = userView.Email,
                PasswordHash = hashedPassword,
                RegistrationDate = userView.RegistrationDate ?? DateTime.UtcNow,
                RoleId = userView.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, userView);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
