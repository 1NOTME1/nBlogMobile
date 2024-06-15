using AppMobilenBlog.ServiceReference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPInBlog.ForView;
using RestAPInBlog.Model;
using RestAPInBlog.Model.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPInBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly nBlogDbContext _context;

        public CommentController(nBlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentForView>>> GetComments()
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return (await _context.Comments.Include(c => c.User).ToListAsync())
                .Select(cm => (CommentForView)cm).ToList();
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentForView>> GetComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return (CommentForView)comment;
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, CommentForView comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }
            var cmdb = (Comment)comment;
            _context.Entry(cmdb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comment
        [HttpPost]
        public async Task<ActionResult<CommentForView>> PostComment(CommentForView comment)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'nBlogDbContext.Comments' is null.");
            }

            var cmdb = (Comment)comment;
            _context.Comments.Add(cmdb);
            await _context.SaveChangesAsync();
            comment.CommentId = cmdb.CommentId;

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
