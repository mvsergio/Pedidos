using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Interfaces
{
	public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}