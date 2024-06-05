using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPInBlog.Model
{
    [Table("users")]
    [Index("Email", Name = "UQ__users__AB6E6164BC9FFE5C", IsUnique = true)]
    [Index("Username", Name = "UQ__users__F3DBC57231AE1918", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; } = null!;
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;
        [Column("registration_date", TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; } = null!;
        [InverseProperty("User")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Like> Likes { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
