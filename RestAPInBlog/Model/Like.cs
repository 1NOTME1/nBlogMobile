using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPInBlog.Model
{
    [Table("likes")]
    public partial class Like
    {
        [Key]
        [Column("like_id")]
        public int LikeId { get; set; }
        [Column("post_id")]
        public int? PostId { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Likes")]
        public virtual Post? Post { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Likes")]
        public virtual User? User { get; set; }
    }
}
