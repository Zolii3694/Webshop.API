using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webshop.API.Data;
using Webshop.API.Models;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class CheckoutController : Controller
    {

        private readonly WebshopDbContext _context;
        private const string CartSessionKey = "Cart";

        public CheckoutController(WebshopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cartItems = GetCart();

            var vm = new CheckoutViewModel
            {
                Items = cartItems,
                TotalPrice = cartItems.Sum(i => i.SubTotal)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            var cartItems = GetCart();

            model.Items = cartItems;
            model.TotalPrice = cartItems.Sum(i => i.SubTotal);

            if (!ModelState.IsValid)
                return View(model);

            if (!cartItems.Any())
            {
                ModelState.AddModelError("", "A kosar ures.");
                return View(model);
            }

            var order = new Order
            {
                CustomerName = model.FullName,
                Email = model.Email,
                Address = $"{model.ZipCode} {model.City}, {model.Address}"
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                if(product == null || product.IsDeleted)
                {
                    ModelState.AddModelError("", $"A termek nem talalhato: {item.ProductName}");
                    return View(model);
                }

                if(product.Stock < item.Quantity)
                {
                    ModelState.AddModelError("", $"Nincs eleg a keszleten: {product.Name}");
                    return View(model);
                }
            
                product.Stock -= item.Quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

        private List<CartItemViewModel> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);

            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItemViewModel>();
            }
            
            return JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson) ?? new List<CartItemViewModel>();
        }
    }
}