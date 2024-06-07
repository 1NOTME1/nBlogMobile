using AppMobilenBlog.Models;
using AppMobilenBlog.ViewModels;
using Xamarin.Forms;

namespace AppMobilenBlog.Views
{
    public partial class NewPostPage : ContentPage
    {
        public Post Post { get; set; }

        public NewPostPage()
        {
            InitializeComponent();
            BindingContext = new NewPostViewModel();
        }
    }
}