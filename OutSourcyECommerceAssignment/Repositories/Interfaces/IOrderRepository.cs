using OutSourcyECommerceAssignment.Models;

namespace OutSourcyECommerceAssignment.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();


    }
}
