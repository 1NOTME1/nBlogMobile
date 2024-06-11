using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPInBlog.ForView;
using RestAPInBlog.Model;
using RestAPInBlog.Model.Context;

namespace RestAPInBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly nBlogDbContext _context;

        public PostController(nBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostForView>>> GetPosts()
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            return (await _context.Posts.ToListAsync()).Select(po => (PostForView)po).ToList();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostForView>> GetPost(int id)
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return (PostForView)post;
        }

        // PUT: api/Post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostForView postView)
        {
            if (id != postView.PostId)
                return BadRequest();

            var existingPost = await _context.Posts.FindAsync(id);
            if (existingPost == null)
                return NotFound();

            existingPost.Title = postView.Title;
            existingPost.Content = postView.Content;
            existingPost.PublicationDate = postView.PublicationDate ?? existingPost.PublicationDate;

            _context.Entry(existingPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostForView>> PostPost(PostForView postView)
        {
          if (_context.Posts == null)
          {
              return Problem("Entity set 'nBlogDbContext.Posts'  is null.");
          }

            var post = new Post
            {
                UserId = postView.UserId,  // Assuming that the userId is passed correctly and exists
                Title = postView.Title,
                Content = postView.Content,
                PublicationDate = postView.PublicationDate ?? DateTime.UtcNow, // Set default to now if not provided
                                                                               // Assume Category and Tags are handled separately or are not required at creation
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, postView);
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
