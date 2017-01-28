using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models.PostViewModels
{
    public class PostDetailViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public String UrlSlug { get; set; }
        public String Created { get; set; }
        public String Modified { get; set; }
        public String Contents { get; set; }
        public String HeaderImgUrl { get; set; }

    }
}
