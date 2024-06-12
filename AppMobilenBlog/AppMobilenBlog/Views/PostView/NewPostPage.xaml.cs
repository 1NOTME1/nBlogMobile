using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.PostViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobilenBlog.Views.PostView
{
    public partial class NewPostPage : ContentPage
    {
        public PostForView Post { get; set; }

        public NewPostPage()
        {
            InitializeComponent();
            BindingContext = new NewPostViewModel();
        }
    }
}