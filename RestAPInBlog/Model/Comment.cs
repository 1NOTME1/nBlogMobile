using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPInBlog.Model
{
    [Table("comments")]
    public partial class Comment
    {
        [Key]
        [Column("comment_id")]
        public int CommentId { get; set; }
        [Column("post_id")]
        public int? PostId { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("content")]
        public string Content { get; set; } = null!;
        [Column("comment_date", TypeName = "datetime")]
        public DateTime? CommentDate { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public virtual Post? Post { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comments")]
        public virtual User? User { get; set; }
    }
}
