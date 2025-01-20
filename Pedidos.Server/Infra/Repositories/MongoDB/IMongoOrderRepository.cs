using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Infra.Repositories.MongoDB
{
    public interface IMongoOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task SaveOrderAsync(Order order);
    }
}
