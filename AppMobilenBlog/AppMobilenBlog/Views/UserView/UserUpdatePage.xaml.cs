using AppMobilenBlog.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobilenBlog.Views.UserView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserUpdatePage : ContentPage
    {
        public UserUpdatePage()
        {
            InitializeComponent();
            BindingContext = new UserUpdateViewModel();
        }
    }
}