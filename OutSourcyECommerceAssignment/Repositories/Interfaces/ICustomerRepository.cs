using OutSourcyECommerceAssignment.Models;

namespace OutSourcyECommerceAssignment.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> AddAsync(Customer customer);

    }
}
