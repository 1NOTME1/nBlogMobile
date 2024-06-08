using RestAPInBlog.Helpers;
using RestAPInBlog.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RestAPInBlog.ForView
{
    public class UserForView
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; } // Nazwa roli użytkownika
        public int PostCount { get; set; } // Liczba postów użytkownika
        public int CommentCount { get; set; } // Liczba komentarzy użytkownika
        public int LikeCount { get; set; } // Liczba polubień użytkownika

        public static implicit operator User(UserForView cli)
             => new User().CopyProperties(cli);

        public static implicit operator UserForView(User source)
            => new UserForView().CopyProperties(source);
    }
}
