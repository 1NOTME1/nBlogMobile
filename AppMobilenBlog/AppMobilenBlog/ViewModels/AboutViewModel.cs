using System.Windows.Input;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            NavigateHomeCommand = new Command(OnNavigateHome);
            NavigatePostsCommand = new Command(OnNavigatePosts);
            NavigateUsersCommand = new Command(OnNavigateUsers);
        }

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigatePostsCommand { get; }
        public ICommand NavigateUsersCommand { get; }

        private async void OnNavigateHome()
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }

        private async void OnNavigatePosts()
        {
            await Shell.Current.GoToAsync("//PostsPage");
        }

        private async void OnNavigateUsers()
        {
            await Shell.Current.GoToAsync("//UsersPage");
        }
    }
}
