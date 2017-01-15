using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String UrlSlug { get; set; }
        public virtual String Description { get; set; }

        public virtual IList<Post> Posts { get; set; }

    }
}
