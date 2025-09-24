using OutSourcyECommerceAssignment.Models;

namespace OutSourcyECommerceAssignment.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
    }
}
