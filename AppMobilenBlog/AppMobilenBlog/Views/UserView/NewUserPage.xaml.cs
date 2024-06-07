using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.UserViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobilenBlog.Views.UserView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserPage : ContentPage
    {
        public User User {  get; set; }

        public NewUserPage()
        {
            InitializeComponent();
            BindingContext = new NewUserViewModel();
        }
    }
}