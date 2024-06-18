using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using System;
using Xamarin.Essentials;

namespace AppMobilenBlog.ViewModels.PostViewModel
{
    public class NewPostViewModel : ANewItemViewModel<PostForView>
    {
        #region Fields
        private string title;
        private string content;
        private string tagData;
        private string userName;
        private string categoryData;
        private DateTime publicationDate;
        private int userId;
        #endregion
        #region Construktor
        public NewPostViewModel()
            : base("Add New Post")
        {
            PublicationDate = DateTime.Now;
            UserId = Preferences.Get("UserId", 0);
        }
        #endregion
        #region Properties
        public int UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
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

        public string TagData
        {
            get => tagData;
            set => SetProperty(ref tagData, value);
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

        public DateTime PublicationDate
        {
            get => publicationDate;
            set => SetProperty(ref publicationDate, value);
        }
        #endregion
        #region Methods
        public override bool ValidateSave() => !string.IsNullOrWhiteSpace(title)
            && !string.IsNullOrWhiteSpace(content)
            && !string.IsNullOrWhiteSpace(tagData)
            && !string.IsNullOrWhiteSpace(userName)
            && !string.IsNullOrWhiteSpace(categoryData);

        public override PostForView SetItem()
        {
            return new PostForView()
            {
                PostId = 0,
                UserId = userId,
                Title = title,
                Content = content,
                TagData = tagData,
                UserName = userName,
                CategoryData = categoryData,
                PublicationDate = publicationDate
            };
        }
        #endregion
    }
}
