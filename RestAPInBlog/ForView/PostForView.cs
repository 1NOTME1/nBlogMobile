using RestAPInBlog.Model;
using System;
using System.Collections.Generic;

namespace RestAPInBlog.ForView
{
    public class PostForView
    {
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string CategoryData { get; set; }
        public string TagData { get; set; } // Add TagData property
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

        public static implicit operator PostForView(Post source)
        {
            return new PostForView
            {
                PostId = source.PostId,
                UserId = source.UserId,
                UserName = source.User?.Username ?? string.Empty,
                Title = source.Title,
                Content = source.Content,
                PublicationDate = source.PublicationDate,
                CategoryData = string.Join(", ", source.Categories.Select(c => c.CategoryName)),
                TagData = string.Join(" ", source.Tags.Select(t => "#" + t.TagName)),
                CommentCount = source.Comments?.Count ?? 0,
                LikeCount = source.Likes?.Count ?? 0
            };
        }

        public static implicit operator Post(PostForView view)
        {
            if (view == null) return null;

            var post = new Post
            {
                PostId = view.PostId,
                UserId = view.UserId,
                Title = view.Title,
                Content = view.Content,
                PublicationDate = view.PublicationDate
            };

            return post;
        }
    }
}
