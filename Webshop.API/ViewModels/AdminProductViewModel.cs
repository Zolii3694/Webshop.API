using System.ComponentModel.DataAnnotations;

namespace Webshop.API.ViewModels
{
    public class AdminProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Termék neve")]
        public string Name { get; set; } = "";

        [Required]
        [Display(Name = "Leírás")]
        public string Description { get; set; } = "";

        [Required]
        [Display(Name = "Ár")]
        public decimal Price { get; set; }

        [Display(Name = "Kép URL")]
        public string ImageUrl { get; set; } = "";

        [Required]
        [Display(Name = "Készlet")]
        public int Stock { get; set; }
    }
}