using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Webshop.API.Data;
using Webshop.API.Models;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }
        private readonly WebshopDbContext _context;

        public AdminController(WebshopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "Account");
            }

            var vm = new AdminProductViewModel
            {
                Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdminProductViewModel vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    })
                    .ToList();

                return View(vm);
            }

            var product = new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                ImageUrl = vm.ImageUrl,
                Stock = vm.Stock,
                CategoryId = vm.CategoryId,
                IsDeleted = false
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}