using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.API.Data;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class ShopController : Controller
    {
        private readonly WebshopDbContext _context;

        public ShopController(WebshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).Where(p => !p.IsDeleted).ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Orders()
        {
            return View(new List<OrderViewModel>());
        }
    }
}
