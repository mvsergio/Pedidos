using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Infra.Repositories.SqlServer
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}