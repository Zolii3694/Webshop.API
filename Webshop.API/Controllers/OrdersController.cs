using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop.API.Data;
using Webshop.API.Models;
using Webshop.API.Data_Transfer_Object;
using Microsoft.AspNetCore.Authorization;

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WebshopDbContext _context;

        public OrdersController(WebshopDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToListAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product)
                                             .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
            {
                return BadRequest("A rendeles nem lehet ures");
            }

            var order = new Order
            {
                CustomerName = dto.CustomerName,
                Email = dto.Email,
                Address = dto.Address,
                OrderDate = DateTime.Now,
            };

            foreach (var itemDto in dto.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == itemDto.ProductId && !p.IsDeleted);


                if (product == null)
                {
                    return BadRequest($"Nem letezo vagy torolt termek ID: {itemDto.ProductId}");
                }

                if (product.Stock < itemDto.Quantity)
                {
                    return BadRequest($"Nincs eleg a keszleten: {product.Name}, elerheto: {itemDto.Stock}");
                }

                product.Stock -= itemDto.Quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
    }
}
