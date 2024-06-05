using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using WCFnBlogServices.Model.Entities;

namespace WCFnBlogServices
{
    [ServiceContract]
    public interface InBlogServices
    {
        [OperationContract]
        List<PostForView> GetPost();

        [OperationContract]
        List<PostForView> GetItemSortByTitle();
    }

    [DataContract]
    public class PostForView
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime PublicationDate { get; set; }

        public PostForView() { }

        public PostForView(posts post)
        {
            PostId = post.post_id;
            UserId = post.user_id.Value;
            Title = post.title;
            Content = post.content;
            PublicationDate = post.publication_date.Value;
        }
    }


}
