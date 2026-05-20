using Microsoft.AspNetCore.Mvc;

namespace Webshop.API.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
