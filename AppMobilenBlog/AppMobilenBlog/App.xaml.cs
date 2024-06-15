using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using Xamarin.Forms;

namespace AppMobilenBlog
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<ItemDataStore>();
            DependencyService.Register<UserDataStore>();
            DependencyService.Register<IDataStore<UserForView>, UserDataStore>();
            DependencyService.Register<IDataStore<PostForView>, PostDataStore>();
            DependencyService.Register<ILoginService, LoginService>();
            DependencyService.Register<CommentDataStore>();
            DependencyService.Register<ICommentDataStore, CommentDataStore>();

            MainPage = new NavigationPage(new Views.LoginPage());
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void SetMainPageAfterLogin()
        {
            MainPage = new AppShell();
        }
    }
}
