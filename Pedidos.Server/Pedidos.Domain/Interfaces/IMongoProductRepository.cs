using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Interfaces
{
	public interface IMongoProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task SaveProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
