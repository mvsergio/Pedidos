using Microsoft.EntityFrameworkCore;
using Pedidos.Server.Application.CQRS.EventHandler;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Domain.Events;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Infra.Repositories.SqlServer
{
    public class OrderRepository(ApplicationDbContext context,
        OrderCreatedEventHandler orderCreatedEventHandler,
        OrderUpdatedEventHandler orderUpdatedEventHandler,
        OrderDeletedEventHandler orderDeletedEventHandler
            ) : IOrderRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly OrderCreatedEventHandler _orderCreatedEventHandler = orderCreatedEventHandler;
        private readonly OrderUpdatedEventHandler _orderUpdatedEventHandler = orderUpdatedEventHandler;
        private readonly OrderDeletedEventHandler _orderDeletedEventHandler = orderDeletedEventHandler;

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
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

            // Trigger event for synchronization
            var orderCreatedEvent = new OrderCreatedEvent(order.Id);
            await _orderCreatedEventHandler.Handle(orderCreatedEvent);

            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            // Trigger the update event
            var orderUpdatedEvent = new OrderUpdatedEvent(order.Id);
            await _orderUpdatedEventHandler.Handle(orderUpdatedEvent);

            return order;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                // Trigger the delete event
                var orderDeletedEvent = new OrderDeletedEvent(id);
                await _orderDeletedEventHandler.Handle(orderDeletedEvent);
            }
        }
    }
}