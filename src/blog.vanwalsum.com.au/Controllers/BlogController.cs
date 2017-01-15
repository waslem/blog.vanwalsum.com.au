using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace blog.vanwalsum.com.au.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post(int? id, string postName)
        {
            return View();
        }
    }
}