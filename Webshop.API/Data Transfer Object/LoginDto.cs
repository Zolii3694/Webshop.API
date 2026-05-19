using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Data_Transfer_Object
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
