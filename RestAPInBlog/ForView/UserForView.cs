using RestAPInBlog.Model;
using System;

namespace RestAPInBlog.ForView
{
    public class UserForView
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public int PostCount { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
        public string? Password { get; set; }


        public static implicit operator User(UserForView view)
        {
            if (view == null) return null;
            var user = new User
            {
                UserId = view.UserId,
                Username = view.Username,
                Email = view.Email,
                RegistrationDate = view.RegistrationDate,
                RoleId = view.RoleId,
                PasswordHash = view.Password
                
            };
            if (!string.IsNullOrWhiteSpace(view.Password))
            {
                user.PasswordHash = view.Password;
            }
            return user;
        }

        public static implicit operator UserForView(User user)
        {
            if (user == null) return null;
            return new UserForView
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                RoleId = user.RoleId,
                RoleName = user.Role?.Name,
                PostCount = user.Posts?.Count ?? 0,
                CommentCount = user.Comments?.Count ?? 0,
                LikeCount = user.Likes?.Count ?? 0,
                Password = user.PasswordHash
                
            };
        }
    }
}
