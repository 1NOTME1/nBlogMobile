using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPInBlog.Model
{
    [Table("tags")]
    [Index("TagName", Name = "UQ__tags__E298655C0EBB9870", IsUnique = true)]
    public partial class Tag
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("tag_id")]
        public int TagId { get; set; }
        [Column("tag_name")]
        [StringLength(50)]
        public string TagName { get; set; } = null!;

        [ForeignKey("TagId")]
        [InverseProperty("Tags")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
