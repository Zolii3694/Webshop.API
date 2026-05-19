using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Data_Transfer_Object
{
    public class CreateOrderDto
    {
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address {  get; set; } = string.Empty;

        [Required]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
