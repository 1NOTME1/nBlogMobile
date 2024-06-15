using AppMobilenBlog.ServiceReference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPInBlog.Model;
using RestAPInBlog.Model.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPInBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly nBlogDbContext _context;

        public LikeController(nBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Like
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeForView>>> GetLikes()
        {
            if (_context.Likes == null)
            {
                return NotFound();
            }
            return (await _context.Likes.ToListAsync()).Select(li => (LikeForView)li).ToList();
        }

        // GET: api/Like/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikeForView>> GetLike(int id)
        {
            if (_context.Likes == null)
            {
                return NotFound();
            }
            var like = await _context.Likes.FindAsync(id);

            if (like == null)
            {
                return NotFound();
            }

            return (LikeForView)like;
        }

        // PUT: api/Like/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, LikeForView like)
        {
            if (id != like.LikeId)
            {
                return BadRequest();
            }

            _context.Entry(like).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
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

        // POST: api/Like
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikeForView>> PostLike(LikeForView likeView)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'nBlogDbContext.Likes' is null.");
            }
            var like = new Like
            {
                // Przypisz wartości z likeView do nowego obiektu Like, upewnij się, że wszystkie wymagane pola są ustawione
                PostId = likeView.PostId,
                UserId = likeView.UserId
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            // Ustaw ID po zapisaniu w DB
            likeView.LikeId = like.LikeId;

            return CreatedAtAction("GetLike", new { id = like.LikeId }, likeView);
        }


        // DELETE: api/Like/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            if (_context.Likes == null)
            {
                return NotFound();
            }
            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LikeExists(int id)
        {
            return (_context.Likes?.Any(e => e.LikeId == id)).GetValueOrDefault();
        }
    }
}
