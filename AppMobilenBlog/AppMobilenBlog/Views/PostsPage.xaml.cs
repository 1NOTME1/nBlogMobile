using AppMobilenBlog.Models;
using AppMobilenBlog.ViewModels;
using AppMobilenBlog.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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