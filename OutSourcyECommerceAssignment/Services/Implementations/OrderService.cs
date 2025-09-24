using OutSourcyECommerceAssignment.Models;
using OutSourcyECommerceAssignment.Repositories.Interfaces;
using OutSourcyECommerceAssignment.Services.Interfaces;

namespace OutSourcyECommerceAssignment.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }
        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }
        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            await _orderRepository.UpdateOrderStatusAsync(id, newStatus);
        }
    }
}
