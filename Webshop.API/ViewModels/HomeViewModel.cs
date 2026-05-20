using Webshop.API.Models;

namespace Webshop.API.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}