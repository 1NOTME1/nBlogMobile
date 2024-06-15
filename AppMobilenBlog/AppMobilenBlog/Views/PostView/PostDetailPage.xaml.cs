using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.ViewModels.PostViewModel;
using Xamarin.Forms;

namespace AppMobilenBlog.Views
{
    public partial class PostDetailPage : ContentPage
    {
        public PostDetailPage()
        {
            InitializeComponent();
            BindingContext = new PostDetailViewModel(DependencyService.Get<IDataStore<PostForView>>());
        }
    }

}