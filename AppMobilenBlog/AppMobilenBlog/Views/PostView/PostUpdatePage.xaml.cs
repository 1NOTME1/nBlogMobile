using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.ViewModels.PostViewModel;
using AppMobilenBlog.ViewModels.UserViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobilenBlog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostUpdatePage : ContentPage
    {
        public PostUpdatePage()
        {
            InitializeComponent();
            var dataStore = new PostDataStore();
            BindingContext = new PostUpdateViewModel(dataStore);
        }
    }
}