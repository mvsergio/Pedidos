using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Domain.Events;
using Pedidos.Server.Infra.Data;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.EventHandler
{
    public class OrderCreatedEventHandler(IOrderRepository orderRepository, IMongoDbContext mongoDbContext)
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMongoDbContext _mongoDbContext = mongoDbContext;

        public async Task Handle(Order orderCreatedEvent)
        {
            // Get the order from SQL Server
            var order = await _orderRepository.GetByIdAsync(orderCreatedEvent.Id);

            if (order != null)
            {
                // Insert into MongoDB
                var ordersCollection = _mongoDbContext.GetCollection<Order>("Orders");
                await ordersCollection.InsertOneAsync(order);
            }
        }
    }
}
