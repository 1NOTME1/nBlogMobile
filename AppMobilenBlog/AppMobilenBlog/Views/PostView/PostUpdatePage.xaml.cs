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
            BindingContext = new PostUpdateViewModel();
        }
    }
}