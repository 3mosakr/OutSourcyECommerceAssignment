namespace OutSourcyECommerceAssignment.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemDto> OrderProducts { get; set; } = new();
    }
}
