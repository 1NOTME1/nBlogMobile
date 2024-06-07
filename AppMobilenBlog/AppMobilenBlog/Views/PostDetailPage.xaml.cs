using AppMobilenBlog.ViewModels;
using Xamarin.Forms;

namespace AppMobilenBlog.Views
{
    public partial class PostDetailPage : ContentPage
    {
        public PostDetailPage()
        {
            InitializeComponent();
            BindingContext = new PostDetailViewModel();
        }
    }
}