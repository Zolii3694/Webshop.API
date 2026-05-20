using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            var productsQuery = _context.Products.Include(p => p.Category).Where(p => !p.IsDeleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(search) || 
                    (p.Description != null && p.Description.Contains(search)));
            }

            if(categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            var model = new HomeViewModel
            {
                Products = await productsQuery.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };



            return View(model);

        }
    }
}
