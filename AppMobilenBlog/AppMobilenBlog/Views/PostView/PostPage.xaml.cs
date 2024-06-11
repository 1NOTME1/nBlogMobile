using AppMobilenBlog.ViewModels.PostViewModel;
using Xamarin.Forms;

namespace AppMobilenBlog.Views
{
    public partial class PostsPage : ContentPage
    {
        PostsViewModel _viewModel;

        public PostsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PostsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}