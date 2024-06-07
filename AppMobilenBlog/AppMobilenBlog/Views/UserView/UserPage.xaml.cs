using AppMobilenBlog.ViewModels.UserViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobilenBlog.Views.UserView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private UserViewModel _viewModel;

        public UserPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new UserViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}