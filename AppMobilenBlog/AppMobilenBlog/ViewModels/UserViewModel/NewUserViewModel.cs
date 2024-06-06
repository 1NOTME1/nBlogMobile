using AppMobilenBlog.Models;
using AppMobilenBlog.Services;
using System;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.UserViewModel
{
    public class NewUserViewModel : BaseViewModel
    {
        public IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>();
        #region Fields
        private int userId;
        private string username;
        private string email;
        private string passwordHash;
        private DateTime registrationDate;
        private int roleId;
        #endregion Fields

        public NewUserViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave() => userId > 0
                && !string.IsNullOrWhiteSpace(username)
                && !string.IsNullOrWhiteSpace(email)
                && !string.IsNullOrWhiteSpace(passwordHash);

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
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        #endregion

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var newUser = new User()
            {
                UserId = UserId,
                Username = Username,
                Email = Email,
                PasswordHash = PasswordHash,
                RegistrationDate = DateTime.Now,
                RoleId = RoleId
            };
            await DataStore.AddItemAsync(newUser);
            await Shell.Current.GoToAsync("..");
        }
    }
}
