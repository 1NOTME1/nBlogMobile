using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMobilenBlog.ViewModels.CommentViewModel;

namespace AppMobilenBlog.Views.CommentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentsPage : ContentPage
    {
        public CommentsPage(int postId, int userId)
        {
            InitializeComponent(); // Initialize components first

            BindingContext = new CommentsViewModel(postId);

            ToolbarItems.Add(new ToolbarItem("Add", null, async () =>
            {
                // Use Shell navigation with query parameters instead of constructor parameters
                var route = $"{nameof(NewCommentPage)}?postId={postId}&userId={userId}";
                await Shell.Current.GoToAsync(route);
            }));
        }
    }
}
