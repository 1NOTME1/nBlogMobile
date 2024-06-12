using AppMobilenBlog.Services;
using AppMobilenBlog.Views;
using AppMobilenBlog.Views.PostView;
using AppMobilenBlog.Views.UserView;
using System;
using Xamarin.Forms;

namespace AppMobilenBlog
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewPostPage), typeof(NewPostPage));
            Routing.RegisterRoute(nameof(PostDetailPage), typeof(PostDetailPage));
            Routing.RegisterRoute(nameof(PostUpdatePage), typeof(PostUpdatePage));

            Routing.RegisterRoute(nameof(NewUserPage), typeof(NewUserPage));
            Routing.RegisterRoute(nameof(UserUpdatePage), typeof(UserUpdatePage));
            Routing.RegisterRoute(nameof(UserDetailPage), typeof(UserDetailPage));

            DependencyService.Register<ILoginService, LoginService>();

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
            => await Current.GoToAsync("//LoginPage");
    }
}
