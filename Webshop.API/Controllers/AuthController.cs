using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Webshop.API.Data;
using Webshop.API.Data_Transfer_Object;
using Webshop.API.Models;

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WebshopDbContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IConfiguration _config;

        public AuthController(
            WebshopDbContext context,
            IPasswordHasher<Users> passwordHasher,
            IConfiguration config)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if(existingUser != null)
            {
                return BadRequest("Ez a felhasznalonev mar foglalt.");
            }

            var user = new Users
            {
                UserName = dto.UserName,
                Role = dto.Role == "Admin" ? "Admin" : "User"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Sikeres regisztracio.",
                user.UserName,
                user.Role
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync( u => u.UserName == dto.UserName);

            if(user == null)
            {
                return Unauthorized("Hibas felhasznalonev vagy jelszo.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Hibas felhasznalonev vagy jelszo.");
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.UserName,
                    user.Role
                }
            });
        }

        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires : DateTime.Now.AddHours(2),
                signingCredentials : credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
