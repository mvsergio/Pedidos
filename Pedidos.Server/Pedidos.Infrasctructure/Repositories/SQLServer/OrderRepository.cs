using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;
using Pedidos.Infrasctructure.Data;

namespace Pedidos.Infra.Repositories.SqlServer
{
	public class OrderRepository(ApplicationDbContext context) : IOrdersRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public IQueryable<Order> FilterAllAsync()
        {
            return _context.Orders.Include(o => o.Items);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }


        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}