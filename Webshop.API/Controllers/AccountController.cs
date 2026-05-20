using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.API.Data;
using Webshop.API.Models;
using Webshop.API.ViewModels;

namespace Webshop.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebshopDbContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public AccountController(WebshopDbContext context, IPasswordHasher<Users> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Hibás felhasználónév vagy jelszó.");
                return View(model);
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Hibás felhasználónév vagy jelszó.");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "Ez a felhasználónév már foglalt.");
                return View(model);
            }

            var user = new Users
            {
                UserName = model.UserName,
                Role = "User"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}