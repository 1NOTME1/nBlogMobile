using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMobilenBlog.ViewModels;
using AppMobilenBlog.Services;
using Xamarin.Essentials;

namespace AppMobilenBlog.Views.CommentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PostId), "postId")]  // Deklaracja QueryProperty dla PostId
    [QueryProperty(nameof(UserId), "userId")]  // Deklaracja QueryProperty dla UserId
    public partial class NewCommentPage : ContentPage
    {
        public NewCommentPage()
        {
            InitializeComponent();
        }

        private int postId;
        public int PostId
        {
            get => postId;
            set
            {
                postId = value;
                OnPropertyChanged();
            }
        }

        private int userId;
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var commentDataStore = DependencyService.Get<CommentDataStore>();
            BindingContext = new NewCommentViewModel(commentDataStore, PostId, UserId);
        }
    }
}
