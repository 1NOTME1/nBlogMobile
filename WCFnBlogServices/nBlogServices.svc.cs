using System.Collections.Generic;
using System.Linq;
using WCFnBlogServices.Model.Entities;

namespace WCFnBlogServices
{
    public class nBlogServices : InBlogServices
    {
        public List<PostForView> GetPost()
        {
            var db = new nBlogMobileDB();
            var query = from post in db.posts select post;

            return query.ToList()
                .Select(post => new PostForView(post))
                .ToList();
        }

        public List<PostForView> GetItemSortByTitle()
        {
            var db = new nBlogMobileDB();
            var query = from post in db.posts select post;
            query = query.OrderBy(post => post.title);

            return query.ToList()
                .Select(post => new PostForView(post))
                .ToList();
        }
    }
}
