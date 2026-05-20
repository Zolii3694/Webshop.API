using Microsoft.AspNetCore.Mvc;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new CartViewModel();

            return View(vm);
        }
    }
}
