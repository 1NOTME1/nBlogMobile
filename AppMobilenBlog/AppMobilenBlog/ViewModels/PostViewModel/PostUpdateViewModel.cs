using AppMobilenBlog.ViewModels.Abstractions;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.Helpers;

namespace AppMobilenBlog.ViewModels.PostViewModel
{
    public class PostUpdateViewModel : AItemUpdateViewModel<PostForView>
    {
        #region Fields
        private int postId;
        private string title;
        private string content;
        private DateTime? publicationDate;
        private string userName;
        private string categoryData;
        private string tagData;
        private int commentCount;
        private int likeCount;
        private IDataStore<PostForView> DataStore;
        #endregion Fields

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
        public int CommentCount
        {
            get => commentCount;
            set => SetProperty(ref commentCount, value);
        }
        public int LikeCount
        {
            get => likeCount;
            set => SetProperty(ref likeCount, value);
        }
        #endregion

        #region Constructor
        public PostUpdateViewModel(IDataStore<PostForView> dataStore) : base("Post Update")
        {
            DataStore = dataStore;
        }
        #endregion

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    this.CopyProperties(item);
                    PostId = item.PostId;
                    Title = item.Title;
                    Content = item.Content;
                    PublicationDate = item.PublicationDate.HasValue ? item.PublicationDate.Value.DateTime : (DateTime?)null;
                    UserName = item.UserName;
                    CategoryData = item.CategoryData;
                    TagData = item.TagData;
                    CommentCount = item.CommentCount;
                    LikeCount = item.LikeCount;

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

        public override bool ValidateSave()
        {
            return PostId > 0 && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Content);
        }

        public override PostForView SetItem()
        {
            return new PostForView
            {
                PostId = this.PostId,
                Title = this.Title,
                Content = this.Content,
                PublicationDate = this.PublicationDate,
                UserName = this.UserName,
                CategoryData = this.CategoryData,
                TagData = this.TagData,
                CommentCount = this.CommentCount,
                LikeCount = this.LikeCount
            };
        }

        public async Task<bool> SaveItemAsync()
        {
            try
            {
                var post = SetItem();

                if (post.PostId > 0)
                {
                    await DataStore.UpdateItemAsync(post);
                }
                else
                {
                    await DataStore.AddItemAsync(post);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Save Post: {ex.Message}");
                return false;
            }
        }
    }
}
