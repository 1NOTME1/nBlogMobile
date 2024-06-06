using AppMobilenBlog.Models;
using AppMobilenBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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