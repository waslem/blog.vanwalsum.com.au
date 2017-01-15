using blog.vanwalsum.com.au.Models.PostViewModels;
using blog.vanwalsum.com.au.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {

        private readonly BlogRepository _repo;

        public SideBarViewComponent(BlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sideBar = _repo.GetSideBar(5);

            return View(sideBar);
        }
    }
}
