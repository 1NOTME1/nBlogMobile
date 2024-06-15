using AppMobilenBlog.ViewModels.PostViewModel;
using Xamarin.Forms;
using System;
using AppMobilenBlog.ServiceReference; // Upewnij się, że masz tę przestrzeń nazw dla obsługi EventArgs

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

        // Metoda obsługująca zdarzenie kliknięcia przycisku "Like"
        private void OnLikeButtonClicked(object sender, EventArgs e)
        {
            // Możesz tu dodać logikę, jaką chcesz wykonać po kliknięciu przycisku
            // Na przykład możesz wywołać odpowiednią metodę w ViewModelu
            if (sender is Button button && button.BindingContext is PostForView post)
            {
                _viewModel.LikePostCommand.Execute(post);
            }
        }
    }
}
