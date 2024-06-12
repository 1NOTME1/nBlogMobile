using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels.PostViewModel
{
    public class PostDetailViewModel : AItemDetailsViewModel<PostForView>
    {
        #region Fields
        private int postId;
        private string title;
        private string content;
        private DateTime? publicationDate;
        private string userName;
        private string categoryData;
        private string tagData;
        #endregion

        #region Constructor
        public PostDetailViewModel()
            : base("Post Details")
        {
        }
        #endregion

        #region Properties
        public int PostId
        {
            get => postId;
            set => SetProperty(ref postId, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        public DateTime? PublicationDate
        {
            get => publicationDate;
            set => SetProperty(ref publicationDate, value);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string CategoryData
        {
            get => categoryData;
            set => SetProperty(ref categoryData, value);
        }

        public string TagData
        {
            get => tagData;
            set => SetProperty(ref tagData, value);
        }
        #endregion

        #region Methods
        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    this.CopyProperties(item);
                    PostId = id;

                    if (item.PublicationDate.HasValue)
                    {
                        PublicationDate = item.PublicationDate.Value.DateTime; // Explicitly convert to DateTime
                    }

                    UserName = item.UserName;
                    CategoryData = item.CategoryData;
                    TagData = item.TagData;

                    Debug.WriteLine($"Loaded post with date: {PublicationDate}");
                }
                else
                {
                    Debug.WriteLine("Failed to load post data, or post data is null.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Load Post: {ex.Message}");
            }
        }

        protected override Task GoToUpdatePage()
            => Shell.Current.GoToAsync($"{nameof(PostUpdatePage)}?{nameof(PostUpdateViewModel.ItemId)}={ItemId}");
        #endregion
    }
}
