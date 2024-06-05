using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPInBlog.Model
{
    [Table("categories")]
    [Index("CategoryName", Name = "UQ__categori__5189E255E3D9F624", IsUnique = true)]
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("category_name")]
        [StringLength(50)]
        public string CategoryName { get; set; } = null!;

        [ForeignKey("CategoryId")]
        [InverseProperty("Categories")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
