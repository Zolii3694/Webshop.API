using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Data_Transfer_Object
{
    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
