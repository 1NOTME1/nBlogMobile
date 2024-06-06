using AppMobilenBlog.ViewModels;
using System.ComponentModel;
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