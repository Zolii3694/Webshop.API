using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1,100_000_000)]
        public decimal Price { get; set; }

        [Range(0, 10000)]
        public int Stock { get; set; }
        public string? ImageUrl {  get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
