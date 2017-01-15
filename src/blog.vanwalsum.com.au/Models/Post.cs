using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual String Title { get; set; }
        public virtual String ShortDescription { get; set; }
        public virtual String Meta { get; set; }
        public virtual String UrlSlug { get; set; }
        public virtual bool published { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual String SummaryImageUrl { get; set; }
        public virtual String HeaderImageUrl { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<TagPosts> TagPosts { get; set; }
        public virtual String Contents { get; set; }
    }
}
