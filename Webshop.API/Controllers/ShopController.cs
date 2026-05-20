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
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    Email = o.Email,
                    Address = o.Address,
                    OrderDate = o.OrderDate,
                    Items = o.Items.Select(i => new OrderItemViewModel
                    {
                        ProductName = i.Product != null ? i.Product.Name : "Ismeretlen termek",
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList(),
                    TotalPrice = o.Items.Sum(i => i.UnitPrice * i.Quantity)
                })
                .ToListAsync();

            return View(orders);
        }
    }
}
