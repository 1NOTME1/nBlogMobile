using AppMobilenBlog.ServiceReference;
using AppMobilenBlog.ViewModels.Abstractions;
using System;

namespace AppMobilenBlog.ViewModels
{
    public class NewPostViewModel : ANewItemViewModel<PostForView>
    {

        private string title;
        private string content;

        public NewPostViewModel()
            :base("Add New Post")
        {
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

        public override bool ValidateSave() => !String.IsNullOrWhiteSpace(title)
            && !String.IsNullOrWhiteSpace(content);

        public override PostForView SetItem()
        => new PostForView()
        {
            PostId = 0,
            UserId = 0,
            Title = title,
            Content = content,
            PublicationDate = DateTime.Now
        };
    }
}
