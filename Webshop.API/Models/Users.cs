using System.ComponentModel.DataAnnotations;

namespace Webshop.API.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role {  get; set; } = "User";
    }
}
