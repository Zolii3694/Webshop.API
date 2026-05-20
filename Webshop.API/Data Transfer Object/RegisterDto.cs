using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Data_Transfer_Object
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 12)]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
