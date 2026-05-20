using Microsoft.AspNetCore.Mvc;
using Webshop.API.Data;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebshopDbContext _context;

        public HomeController(WebshopDbContext context)
    {
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Products = _context.Products.ToList(),
                Categories = _context.Categories.ToList()
            };

            return View(vm);
        }
    }
}
