using RestAPInBlog.Model;

public class CommentForView
{
    public int CommentId { get; set; }
    public int? PostId { get; set; }
    public int? UserId { get; set; }
    public string Content { get; set; } // Pole wymagane
    public DateTime? CommentDate { get; set; }
    public string UserName { get; set; }

    public static implicit operator CommentForView(Comment source)
    {
        return new CommentForView
        {
            CommentId = source.CommentId,
            PostId = source.PostId,
            UserId = source.UserId,
            Content = source.Content,
            CommentDate = source.CommentDate,
            UserName = source.User?.Username ?? "DefaultUserName" // Upewnij się, że to pole jest poprawnie ustawione
        };
    }


    public static implicit operator Comment(CommentForView view)
    {
        if (view == null) return null;

        var comment = new Comment
        {
            CommentId = view.CommentId,
            PostId = view.PostId,
            UserId = view.UserId,
            Content = view.Content,
            CommentDate = view.CommentDate
        };

        return comment;
    }
}
