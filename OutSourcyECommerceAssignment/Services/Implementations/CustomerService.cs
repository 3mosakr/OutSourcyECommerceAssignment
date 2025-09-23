using OutSourcyECommerceAssignment.Data;
using OutSourcyECommerceAssignment.Models;
using OutSourcyECommerceAssignment.Repositories.Interfaces;
using OutSourcyECommerceAssignment.Services.Interfaces;

namespace OutSourcyECommerceAssignment.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

       

        public async Task<IEnumerable<Customer>> GetAllCusomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public Task<Customer> AddAsync(Customer customer)
        {
            return _customerRepository.AddAsync(customer);
        }
    }
}
