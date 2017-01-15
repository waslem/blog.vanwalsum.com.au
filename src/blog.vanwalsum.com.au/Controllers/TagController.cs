using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace blog.vanwalsum.com.au.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(String id)
        {
            return View();
        }
    }
}