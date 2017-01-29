using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models.Models
{
    public class ApplicationRole : IdentityRole
    {
        public String Description { get; set; }
        public DateTime Created { get; set; }
    }
}
