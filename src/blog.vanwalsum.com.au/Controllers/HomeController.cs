using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using blog.vanwalsum.com.au.Data;
using blog.vanwalsum.com.au.Models.Repository;
using blog.vanwalsum.com.au.Models.PostViewModels;

namespace blog.vanwalsum.com.au.Controllers
{
    public class HomeController : Controller
    {

        private readonly BlogRepository _repo;

        public HomeController(BlogRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            List<PostSummaryViewModel> model = new List<PostSummaryViewModel>();

            model = _repo.GetTopPostSummaries(0).ToList();


            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
