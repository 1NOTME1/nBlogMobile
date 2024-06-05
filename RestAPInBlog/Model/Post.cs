using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPInBlog.Model
{
    [Table("posts")]
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Categories = new HashSet<Category>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("title")]
        [StringLength(200)]
        public string Title { get; set; } = null!;
        [Column("content")]
        public string Content { get; set; } = null!;
        [Column("publication_date", TypeName = "datetime")]
        public DateTime? PublicationDate { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Posts")]
        public virtual User? User { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Like> Likes { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Posts")]
        public virtual ICollection<Category> Categories { get; set; }
        [ForeignKey("PostId")]
        [InverseProperty("Posts")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
