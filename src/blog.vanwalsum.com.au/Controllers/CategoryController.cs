using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.vanwalsum.com.au.Models.Repository;

namespace blog.vanwalsum.com.au.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BlogRepository _repo;

        public CategoryController(BlogRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var model = _repo.GetCategorySummary();

            return View(model);
        }
        
        public IActionResult Detail(String id)
        {
            var model = _repo.GetPostSummariesByCategory(id);
            ViewData["Category"] = id;
            return View(model);
        }
    }
}