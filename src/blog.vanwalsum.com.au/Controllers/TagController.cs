using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.vanwalsum.com.au.Models.Repository;

namespace blog.vanwalsum.com.au.Controllers
{
    public class TagController : Controller
    {
        private readonly BlogRepository _repo;
        public TagController(BlogRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var model = _repo.GetTagSummary();

            return View(model);
        }
        public IActionResult Detail(string id)
        {

            var model = _repo.GetPostSummariesByTag(id);
            ViewData["Tag"] = id;
            return View(model);
        }
    }
}