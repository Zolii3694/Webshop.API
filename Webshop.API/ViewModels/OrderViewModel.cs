namespace Webshop.API.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string Email { get; set; } = "";

        public string Address { get; set; } = "";

        public DateTime OrderDate { get; set; }

        public List<OrderItemViewModel> Items { get; set; } = new();

        public decimal TotalPrice { get; set; }
    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}