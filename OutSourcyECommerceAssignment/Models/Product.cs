namespace OutSourcyECommerceAssignment.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<OrderProduc> OrderProducts { get; set; } = new List<OrderProduc>();
    }
}
