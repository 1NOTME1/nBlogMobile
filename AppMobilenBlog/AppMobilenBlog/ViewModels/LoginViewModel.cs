using AppMobilenBlog.Services;
using Xamarin.Forms;
using System.Diagnostics;

namespace AppMobilenBlog.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email;
        private string password;
        public Command LoginCommand { get; }
        private readonly ILoginService _loginService;

        public LoginViewModel()
        {
            _loginService = DependencyService.Get<ILoginService>();
            LoginCommand = new Command(OnLoginClicked);
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

        private async void OnLoginClicked()
        {
            Debug.WriteLine("Login button clicked");

            bool loginResult = false;

            if (_loginService == null)
            {
                Debug.WriteLine("_loginService is null");
            }
            else
            {
                Debug.WriteLine("_loginService is not null");
                loginResult = await _loginService.LoginAsync(Email, Password);
            }

            if (loginResult)
            {
                Debug.WriteLine("Login successful, navigating to AppShell");

                if (Application.Current != null)
                {
                    Debug.WriteLine("Application.Current is not null");

                    if (Application.Current is App app)
                    {
                        Debug.WriteLine("Application.Current is of type App");
                        app.SetMainPageAfterLogin();
                    }
                    else
                    {
                        Debug.WriteLine("Application.Current is not of type App");
                    }
                }
                else
                {
                    Debug.WriteLine("Application.Current is null");
                }
            }
            else
            {
                Debug.WriteLine("Displaying login failed message");
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid email or password.", "OK");
            }
        }
    }
}
