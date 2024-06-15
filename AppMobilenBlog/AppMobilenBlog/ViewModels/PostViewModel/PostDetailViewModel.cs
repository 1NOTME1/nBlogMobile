using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.ViewModels.Abstractions;
using AppMobilenBlog.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private readonly CategoryTagService _categoryTagService;
        #endregion

        #region Constructor
        public PostDetailViewModel(IDataStore<PostForView> dataStore)
            : base("Post Details")
        {
            _categoryTagService = new CategoryTagService(dataStore);
            RemoveCategoryCommand = new Command<string>(async (categoryName) => await RemoveCategory(categoryName));
            RemoveTagCommand = new Command<string>(async (tagName) => await RemoveTag(tagName));
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

        #region Commands
        public ICommand RemoveCategoryCommand { get; private set; }
        public ICommand RemoveTagCommand { get; private set; }
        #endregion

        #region Methods

        private async Task RemoveCategory(string categoryName)
        {
            await _categoryTagService.RemoveCategoryAsync(PostId, categoryName);
            var categories = CategoryData.Split(',').Select(c => c.Trim()).ToList();
            if (categories.Remove(categoryName))
            {
                CategoryData = string.Join(", ", categories);
                OnPropertyChanged(nameof(CategoryData));
            }
        }

        private async Task RemoveTag(string tagName)
        {
            await _categoryTagService.RemoveTagAsync(PostId, tagName);
            var tags = TagData.Split(' ').Select(t => t.TrimStart('#')).ToList();
            if (tags.Remove(tagName))
            {
                TagData = string.Join(" ", tags.Select(t => "#" + t));
                OnPropertyChanged(nameof(TagData));
            }
        }

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
