using Microsoft.AspNetCore.Mvc;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new CheckoutViewModel
            {
                TotalPrice = 0
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}