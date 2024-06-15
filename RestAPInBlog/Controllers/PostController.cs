using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPInBlog.ForView;
using RestAPInBlog.Model;
using RestAPInBlog.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Categories)
                .Include(p => p.Tags)
                .ToListAsync();

            return posts.Select(po => (PostForView)po).ToList();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostForView>> GetPost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User) // Include User
                .Include(p => p.Categories) // Include Categories
                .Include(p => p.Tags) // Include Tags
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            return (PostForView)post;
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostForView postView)
        {
            if (id != postView.PostId)
                return BadRequest();

            var existingPost = await _context.Posts
                .Include(p => p.Categories)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (existingPost == null)
                return NotFound();

            existingPost.Title = postView.Title;
            existingPost.Content = postView.Content;
            existingPost.PublicationDate = postView.PublicationDate ?? existingPost.PublicationDate;

            // Update Categories
            existingPost.Categories.Clear();
            if (!string.IsNullOrWhiteSpace(postView.CategoryData))
            {
                var categoryNames = postView.CategoryData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim());
                foreach (var categoryName in categoryNames)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
                    if (category == null)
                    {
                        category = new Category { CategoryName = categoryName };
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                    }
                    existingPost.Categories.Add(category);
                }
            }

            // Update Tags
            existingPost.Tags.Clear();
            if (!string.IsNullOrWhiteSpace(postView.TagData))
            {
                var tagNames = postView.TagData.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Where(t => t.StartsWith("#"))
                                    .Select(t => t.Trim('#').Trim());

                foreach (var tagName in tagNames)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                    if (tag == null)
                    {
                        tag = new Tag { TagName = tagName };
                        _context.Tags.Add(tag);
                        await _context.SaveChangesAsync();
                    }
                    existingPost.Tags.Add(tag);
                }
            }

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
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<PostForView>> PostPost(PostForView postView)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'nBlogDbContext.Posts' is null.");
            }

            var post = new Post
            {
                UserId = postView.UserId,
                Title = postView.Title,
                Content = postView.Content,
                PublicationDate = postView.PublicationDate ?? DateTime.UtcNow
            };

            // Handle Categories
            if (!string.IsNullOrWhiteSpace(postView.CategoryData))
            {
                var categoryNames = postView.CategoryData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim());
                foreach (var categoryName in categoryNames)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
                    if (category == null)
                    {
                        category = new Category { CategoryName = categoryName };
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                    }
                    post.Categories.Add(category);
                }
            }

            // Handle Tags with #
            if (!string.IsNullOrWhiteSpace(postView.TagData))
            {
                var tagNames = postView.TagData.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Where(t => t.StartsWith("#"))
                                .Select(t => t.Trim('#').Trim());

                foreach (var tagName in tagNames)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);
                    if (tag == null)
                    {
                        tag = new Tag { TagName = tagName };
                        _context.Tags.Add(tag);
                        await _context.SaveChangesAsync();
                    }
                    post.Tags.Add(tag);
                }
            }

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

            var post = await _context.Posts
                .Include(p => p.Categories)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            // Usunięcie powiązań z kategoriami
            foreach (var category in post.Categories.ToList())
            {
                post.Categories.Remove(category);
            }

            // Usunięcie powiązań z tagami
            foreach (var tag in post.Tags.ToList())
            {
                post.Tags.Remove(tag);
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
