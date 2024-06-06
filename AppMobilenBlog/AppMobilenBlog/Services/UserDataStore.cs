using AppMobilenBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobilenBlog.Services
{
    public class UserDataStore : IDataStore<User>
    {

        readonly List<User> users;

        public UserDataStore()
        {
            users = new List<User>()
            {
                new User { UserId = 1, Username = "User1", Email = "user1@example.com", PasswordHash = "hash1", RegistrationDate = DateTime.Now, RoleId = 1 },
                new User { UserId = 2, Username = "User2", Email = "user2@example.com", PasswordHash = "hash2", RegistrationDate = DateTime.Now.AddDays(-1), RoleId = 2 },
                new User { UserId = 3, Username = "User3", Email = "user3@example.com", PasswordHash = "hash3", RegistrationDate = DateTime.Now.AddDays(-2), RoleId = 3 }
            };

        }

        public async Task<bool> AddItemAsync(User item)
        {
            users.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            var oldItem = users.Where((User arg) => arg.UserId == item.UserId).FirstOrDefault();
            users.Remove(oldItem);
            users.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = users.Where((User arg) => arg.UserId == id).FirstOrDefault();
            users.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(int id)
            => await Task.FromResult(users.FirstOrDefault(s => s.UserId == id));

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(users);
    }
}
