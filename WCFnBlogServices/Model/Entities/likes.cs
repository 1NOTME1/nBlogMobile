//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFnBlogServices.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class likes
    {
        public int like_id { get; set; }
        public Nullable<int> post_id { get; set; }
        public Nullable<int> user_id { get; set; }
    
        public virtual posts posts { get; set; }
        public virtual users users { get; set; }
    }
}
