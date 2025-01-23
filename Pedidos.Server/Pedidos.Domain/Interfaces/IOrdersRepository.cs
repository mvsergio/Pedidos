using Pedidos.Domain.Entities;

namespace Pedidos.Domain.Interfaces
{
	public interface IOrdersRepository
    {
        Task<List<Order>> GetAllAsync();
        IQueryable<Order> FilterAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order orderDto);
        Task DeleteAsync(int id);
        Task<Order> UpdateAsync(Order order);
    }
}