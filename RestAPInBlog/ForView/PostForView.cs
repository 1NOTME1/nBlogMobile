using RestAPInBlog.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestAPInBlog.Model;

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
        public string TagData { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

        public static implicit operator PostForView(Post source)
            => new PostForView
            {
                UserName = source.User?.Username ?? string.Empty,
                CategoryData = string.Join(", ", source.Categories.Select(c => c.CategoryName)),
                TagData = string.Join(", ", source.Tags.Select(t => t.TagName)),
                CommentCount = source.Comments.Count,
                LikeCount = source.Likes.Count
            }.CopyProperties(source);


        public static implicit operator Post(PostForView source)
            => new PostForView().CopyProperties(source);
    }
}
