using System.ComponentModel.DataAnnotations;

namespace Webshop.API.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Teljes név")]
        public string FullName { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email cím")]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A jelszavak nem egyeznek.")]
        [Display(Name = "Jelszó megerősítése")]
        public string ConfirmPassword { get; set; } = "";
    }
}