using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using blog.vanwalsum.com.au.Models.Models;

namespace blog.vanwalsum.com.au.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdminController(RoleManager<ApplicationRole> roleManager)
        {
            roleManager = _roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}