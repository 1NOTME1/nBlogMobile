using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.PostViewModel;
using Xamarin.Forms;

namespace AppMobilenBlog.Views
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