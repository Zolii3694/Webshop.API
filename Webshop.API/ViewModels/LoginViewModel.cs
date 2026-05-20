using System.ComponentModel.DataAnnotations;

namespace Webshop.API.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; } = "";
    }
}