using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMobilenBlog.ViewModels.CommentViewModel;
using AppMobilenBlog.Services;
using Xamarin.Essentials;

namespace AppMobilenBlog.Views.CommentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PostId), "postId")]
    [QueryProperty(nameof(UserId), "userId")]
    public partial class NewCommentPage : ContentPage
    {
        private readonly ICommentDataStore _commentDataStore;

        public NewCommentPage()
        {
            InitializeComponent();
            _commentDataStore = DependencyService.Get<ICommentDataStore>();
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
            if (PostId == 0 || UserId == 0)
            {
                PostId = Preferences.Get("CurrentPostId", 0);
                UserId = Preferences.Get("CurrentUserId", 0);
            }
            var viewModel = new NewCommentViewModel(_commentDataStore, PostId, UserId);
            BindingContext = viewModel;
        }
    }
}
