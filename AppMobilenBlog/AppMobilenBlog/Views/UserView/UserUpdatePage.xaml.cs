﻿using AppMobilenBlog.ViewModels.UserViewModel;
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