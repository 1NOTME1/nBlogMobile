using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFnBlog
{
    [ServiceContract]
    public interface InBlogService
    {
        [OperationContract]
        List<PostDTO> GetPosts(); // Pobiera listę postów

        [OperationContract]
        PostDTO GetPostById(int postId); // Pobiera pojedynczy post na podstawie ID

        [OperationContract]
        void CreatePost(PostDTO post); // Tworzy nowy post

        [OperationContract]
        void UpdatePost(PostDTO post); // Aktualizuje istniejący post

        [OperationContract]
        void DeletePost(int postId); // Usuwa post o podanym ID
    }

    [DataContract]
    public class PostDTO // Zmieniamy nazwę klasy na PostDTO
    {
        [DataMember]
        public int PostId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTime PublicationDate { get; set; }
        [DataMember]
        public string Author { get; set; }
        // Dodaj inne właściwości, które chcesz uwzględnić w odpowiedzi (np. kategoria, tagi)
    }
}
