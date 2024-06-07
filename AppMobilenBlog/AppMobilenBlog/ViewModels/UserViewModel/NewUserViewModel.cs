using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using System;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    public class NewUserViewModel : ANewItemViewModel<User>
    {
        #region Fields
        private int userId;
        private string username;
        private string email;
        private string passwordHash;
        private DateTime registrationDate;
        private int roleId;
        #endregion Fields
        public NewUserViewModel() :
            base() {}

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

        public override bool ValidateSave() => userId > 0
                && !string.IsNullOrWhiteSpace(username)
                && !string.IsNullOrWhiteSpace(email)
                && !string.IsNullOrWhiteSpace(passwordHash);

        public override User SetItem()
            => new User().CopyProperties(this);



    }
}
