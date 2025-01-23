using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Interfaces
{
	public interface IMongoOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task SaveOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
