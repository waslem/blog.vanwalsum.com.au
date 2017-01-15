using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models.PostViewModels
{
    public class PostSummaryViewModel
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String ShortDescription { get; set; }
        public String Created { get; set; }
        public String WrittenBy { get; set; }
        public String SummaryImageUrl { get; set; }
        public String Slug { get; set; }
        public String CategoryName { get; set; }

    }

    public class SideBarItems
    {
        public SideBarItems()
        {
            LatestPosts = new List<PostDetails>();
        }

        public List<PostDetails> LatestPosts { get; set; }
        public List<String> Categories { get; set; }
        public List<String> Tags { get; set; }

    }

    public class PostDetails
    {
        public String PostHeader { get; set; }
        public int PostID { get; set; }

        public String PostSlug { get; set; }
    }
}
