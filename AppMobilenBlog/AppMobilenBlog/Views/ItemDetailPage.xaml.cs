using AppMobilenBlog.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppMobilenBlog.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}