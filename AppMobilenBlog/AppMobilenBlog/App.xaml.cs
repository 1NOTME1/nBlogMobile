using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobilenBlog
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ItemDataStore>();
            DependencyService.Register<UserDataStore>();
            DependencyService.Register<IDataStore<User>, UserDataStore>();

            MainPage = new AppShell();
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
    }
}
