using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Views.UserView;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    public class UserDetailsViewModel : AItemDetailsViewModel<UserForView>
    {
        #region Fields
        private int userId;
        private string username;
        private string email;
        private DateTime? registrationDate;
        private int roleId;
        private string roleName;
        private string passwordHash;
        private int postCount;
        private int commentCount;
        private int likeCount;
        #endregion

        #region Constructor
        public UserDetailsViewModel()
            : base("User Details") { }
        #endregion

        #region Properties
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
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

        public string RoleName
        {
            get => roleName;
            set => SetProperty(ref roleName, value);
        }

        public int PostCount
        {
            get => postCount;
            set => SetProperty(ref postCount, value);
        }

        public int CommentCount
        {
            get => commentCount;
            set => SetProperty(ref commentCount, value);
        }

        public int LikeCount
        {
            get => likeCount;
            set => SetProperty(ref likeCount, value);
        }

        protected override Task GoToUpdatePage()
            => Shell.Current.GoToAsync($"{nameof(UserUpdatePage)}?{nameof(UserUpdateViewModel.ItemId)}={ItemId}");

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    this.CopyProperties(item);
                    UserId = id;

                    // Handle nullable DateTimeOffset RegistrationDate
                    if (item.RegistrationDate.HasValue)
                    {
                        RegistrationDate = item.RegistrationDate.Value.DateTime; // Explicitly convert to DateTime
                    }

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
        #endregion
    }
}
