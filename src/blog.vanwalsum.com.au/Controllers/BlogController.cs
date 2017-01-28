using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.vanwalsum.com.au.Models.Repository;

namespace blog.vanwalsum.com.au.Controllers
{
    public class BlogController : Controller
    {

        private readonly BlogRepository _repo;

        public BlogController(BlogRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var model = _repo.GetPostSummaries();

            return View(model);
        }

        public IActionResult Post(int? id, string postName)
        {
            var model = _repo.GetPostDetails(id, postName);
             
            return View(model);
        }
    }
}