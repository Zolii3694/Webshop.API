using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webshop.API.Data;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class CartController : Controller
    {
        private readonly WebshopDbContext _context;
        private const string CartSessionKey = "Cart";

        public CartController(WebshopDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId && !p.IsDeleted);

            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            SaveCart(cart);

            return RedirectToAction("Index", "Cart");
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

            private void SaveCart(List<CartItemViewModel> cart)
            {
                var cartJson = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CartSessionKey, cartJson);
            }
        }
    }

