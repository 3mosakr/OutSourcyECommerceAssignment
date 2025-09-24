using OutSourcyECommerceAssignment.Models;

namespace OutSourcyECommerceAssignment.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderStatusAsync(int id, string newStatus);
    }
}
