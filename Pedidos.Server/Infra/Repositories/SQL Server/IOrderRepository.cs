using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Infra.Repositories.SqlServer
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task DeleteAsync(int id);
        Task<Order> UpdateAsync(Order order);
    }
}