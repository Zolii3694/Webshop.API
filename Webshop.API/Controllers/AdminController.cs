using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<AdminProductViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AdminProductViewModel());
        }

        [HttpPost]
        public IActionResult Create(AdminProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index");
        }
    }
}