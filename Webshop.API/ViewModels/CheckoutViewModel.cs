using System.ComponentModel.DataAnnotations;

namespace Webshop.API.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Teljes név")]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email cím")]
        public string Email { get; set; } = "";

        [Required]
        [Display(Name = "Telefonszám")]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [Display(Name = "Szállítási cím")]
        public string Address { get; set; } = "";

        [Required]
        [Display(Name = "Város")]
        public string City { get; set; } = "";

        [Required]
        [Display(Name = "Irányítószám")]
        public string ZipCode { get; set; } = "";

        [Required]
        [Display(Name = "Fizetési mód")]
        public string PaymentMethod { get; set; } = "";

        public decimal TotalPrice{ get; set; }

    }
}