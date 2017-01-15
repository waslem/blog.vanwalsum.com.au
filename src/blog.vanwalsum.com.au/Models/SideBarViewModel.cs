using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models
{
    public class SideBarViewModel
    {
        public List<String> LatestPosts { get; set; }
        public List<String> Categories { get; set; }
        public List<String> LatestComments { get; set; }
        public List<String> Tags { get; set; }
    }
}
