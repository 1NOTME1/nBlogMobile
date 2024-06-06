using AppMobilenBlog.Models;
using AppMobilenBlog.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class UserDetailsViewModel : BaseViewModel
    {
        public IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>();

        #region Fields
        private int userId;
        private string username;
        private string email;
        private DateTime registrationDate;
        private int roleId;
        private string passwordHash;
        #endregion

        #region Properties
        public int UserId
        {
            get => userId;
            set
            {
                if (userId != value)
                {
                    userId = value;
                    LoadUserId(value);
                }
            }
        }


        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string PasswordHash
        {
            get => passwordHash;
            set => SetProperty(ref passwordHash, value);
        }

        public DateTime RegistrationDate
        {
            get => registrationDate;
            set => SetProperty(ref registrationDate, value);
        }

        public int RoleId
        {
            get => roleId;
            set => SetProperty(ref roleId, value);
        }
        #endregion

        public async void LoadUserId(int userId)
        {
            try
            {
                var user = await DataStore.GetItemAsync(userId);
                if (user != null)
                {
                    UserId = user.UserId;
                    Username = user.Username;
                    Email = user.Email;
                    PasswordHash = user.PasswordHash;
                    RegistrationDate = user.RegistrationDate;
                    RoleId = user.RoleId;
                }
                else
                {
                    Debug.WriteLine("User not found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Load User: {ex.Message}");
            }
        }

    }
}
