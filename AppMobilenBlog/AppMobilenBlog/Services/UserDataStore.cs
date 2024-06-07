using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMobilenBlog.Services
{
    public class UserDataStore : AListDataStore<User>
    {

        public UserDataStore()
        {
            items = new List<User>()
            {
                new User { UserId = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1", RegistrationDate = DateTime.Now, RoleId = 1 },
                new User { UserId = 2, Username = "User2", Email = "user2@example.com", PasswordHash = "hash2", RegistrationDate = DateTime.Now.AddDays(-1), RoleId = 2 },
                new User { UserId = 3, Username = "User3", Email = "user3@example.com", PasswordHash = "hash3", RegistrationDate = DateTime.Now.AddDays(-2), RoleId = 3 }
            };
        }

        public override User Find(User item)
            => items.Where((User arg) => arg.UserId == item.UserId).FirstOrDefault();
        public override User Find(int id)
            => items.Where((User arg) => arg.UserId == id).FirstOrDefault();
    }
}
