using AppMobilenBlog.Views;
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
            Routing.RegisterRoute(nameof(PostDetailPage), typeof(PostDetailPage));
            Routing.RegisterRoute(nameof(NewPostPage), typeof(NewPostPage));
            Routing.RegisterRoute(nameof(UserUpdatePage), typeof(UserUpdatePage));
            Routing.RegisterRoute(nameof(UserDetailPage), typeof(UserDetailPage));
            Routing.RegisterRoute(nameof(NewUserPage), typeof(NewUserPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
            => await Current.GoToAsync("//LoginPage");
    }
}
