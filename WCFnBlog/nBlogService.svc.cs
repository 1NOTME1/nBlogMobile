using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WCFnBlog.Model.Entities;

namespace WCFnBlog
{
    public class BlogService : InBlogService
    {
        // Zastąp "TwojKontekstBazyDanych" rzeczywistą nazwą swojego kontekstu bazy danych
        private nBlogMobileDB _dbContext = new nBlogMobileDB();

        public void CreatePost(PostDTO post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public PostDTO GetPostById(int postId)
        {
            throw new NotImplementedException();
        }

        public List<PostDTO> GetPosts()
        {
            return _dbContext.posts
                .Include(p => p.User) // Dołączamy dane użytkownika (autora)
                .Select(p => new PostDTO
                {
                    PostId = p.post_id,
                    Title = p.title,
                    Content = p.content,
                    PublicationDate = (DateTime)p.publication_date,
                    Author = p.users.username
                })
                .ToList();
        }

        public void UpdatePost(PostDTO post)
        {
            throw new NotImplementedException();
        }

    }
}
