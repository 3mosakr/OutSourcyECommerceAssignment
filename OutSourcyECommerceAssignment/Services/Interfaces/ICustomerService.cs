using OutSourcyECommerceAssignment.Models;

namespace OutSourcyECommerceAssignment.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCusomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> AddAsync(Customer customer);
        Task<bool> EmailExistsAsync(string email);
    }
}
