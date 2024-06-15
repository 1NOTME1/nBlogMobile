using RestAPInBlog.Model;
using System;

namespace AppMobilenBlog.ServiceReference
{
    public class LikeForView
    {
        public int LikeId { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }

        public static implicit operator LikeForView(Like source)
        {
            return new LikeForView
            {
                LikeId = source.LikeId,
                PostId = source.PostId,
                UserId = source.UserId
            };
        }

        public static implicit operator Like(LikeForView view)
        {
            if (view == null) return null;

            var like = new Like
            {
                LikeId = view.LikeId,
                PostId = view.PostId,
                UserId = view.UserId
            };

            return like;
        }
    }
}
