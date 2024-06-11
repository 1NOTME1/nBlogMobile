using AppMobilenBlog.ViewModels.Abstractions;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    public class UserUpdateViewModel : AItemUpdateViewModel<UserForView>
    {
        #region Fields
        private int userId;
        private string username;
        private string email;
        private DateTime? registrationDate;
        private int roleId;
        private string passwordHash;
        #endregion Fields
        #region Properties
        public int UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
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
        public DateTime? RegistrationDate
        {
            get => registrationDate;
            set => SetProperty(ref registrationDate, value);
        }
        public int RoleId
        {
            get => roleId;
            set => SetProperty(ref roleId, value);
        }
        public string PasswordHash
        {
            get => passwordHash;
            set => SetProperty(ref passwordHash, value);
        }
        #endregion
        #region Constructor
        public UserUpdateViewModel()
            : base("User Update")
        {
        }
        #endregion

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    this.CopyProperties(item);
                    UserId = item.UserId;

                    if (item.RegistrationDate.HasValue)
                        RegistrationDate = item.RegistrationDate.Value.DateTime;

                    Debug.WriteLine($"Loaded user with date: {RegistrationDate}");
                }
                else
                {
                    Debug.WriteLine("Failed to load user data, or user data is null.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Load User: {ex.Message}");
            }
        }




        public override bool ValidateSave()
            => UserId > 0 && !string.IsNullOrWhiteSpace(Username);

        public override UserForView SetItem()
        {
            var userForView = new UserForView
            {
                UserId = this.UserId,
                Username = this.Username,
                Email = this.Email,
                RegistrationDate = this.RegistrationDate,
                RoleId = this.RoleId
                // Nie przekazuj PasswordHash
            };
            return userForView;
        }

    }
}
