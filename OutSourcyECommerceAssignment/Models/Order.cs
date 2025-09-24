namespace OutSourcyECommerceAssignment.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public decimal TotalPrice { get; set; } 
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
        public virtual ICollection<OrderProduc>? OrderProducts { get; set; } = new List<OrderProduc>();
    }
}
