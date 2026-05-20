using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class CheckoutController : Controller
    {
        private const string CartSessionKey = "Cart";
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
        public IActionResult Index(CheckoutViewModel model)
        {
            var cartItems = GetCart();

            model.Items = cartItems;
            model.TotalPrice = cartItems.Sum(i => i.SubTotal);

            if (!ModelState.IsValid)
                return View(model);

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