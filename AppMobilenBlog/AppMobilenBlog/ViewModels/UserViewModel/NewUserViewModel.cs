using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using System;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    public class NewUserViewModel : ANewItemViewModel<UserForView>
    {
        #region Fields
        private int userId;
        private string username;
        private string email;
        private string password;
        private DateTime registrationDate;
        private int roleId;
        #endregion Fields
        public NewUserViewModel() : base("Add New User") { }

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

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
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

        public override bool ValidateSave() => userId > 0
                && !string.IsNullOrWhiteSpace(username)
                && !string.IsNullOrWhiteSpace(email)
                && !string.IsNullOrWhiteSpace(password);

        public override UserForView SetItem()
        {
            return new UserForView()
            {
                UserId = this.UserId,
                Username = this.Username,
                Email = this.Email,
                RegistrationDate = this.RegistrationDate,
                RoleId = this.RoleId,
                RoleName = "Default Role",
                PostCount = 0,
                CommentCount = 0,
                LikeCount = 0,
                Password = this.Password
            };
        }
    }
}
