using AppMobilenBlog.Helpers;
using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.Services;
using AppMobilenBlog.ViewModels.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMobilenBlog.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class PostDetailViewModel : AItemDetailsViewModel<PostForView>
    {
        #region Fileds
        private string title;
        private string content;


        public int Id { get; set; }
        public int UserId { get; set; }
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

        public DateTime? PublicationDate { get; set; }
        #endregion
        #region Constructor
        public PostDetailViewModel() 
            :base("Post details")
        {
        }
        #endregion
        #region Methods
        public override async Task LoadItem(int id)
        {
            try
            {
                var item = (await DataStore.GetItemAsync(id));
                this.CopyProperties(item);
                Id = item.PostId;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Post");
            }
        }

        protected override Task GoToUpdatePage()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
