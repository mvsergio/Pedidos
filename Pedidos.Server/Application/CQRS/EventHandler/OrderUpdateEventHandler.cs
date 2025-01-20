using MongoDB.Driver;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Domain.Events;
using Pedidos.Server.Infra.Data;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.EventHandler
{
    public class OrderUpdatedEventHandler(IOrderRepository orderRepository, IMongoDbContext mongoDbContext)
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMongoDbContext _mongoDbContext = mongoDbContext;

        public async Task Handle(OrderUpdatedEvent orderUpdatedEvent)
        {
            // Get the updated order from SQL Server
            var order = await _orderRepository.GetByIdAsync(orderUpdatedEvent.OrderId);

            if (order != null)
            {
                var filter = Builders<Order>.Filter
                    .Eq(o => o.Id, order.Id);

                // Update the order in MongoDB
                var ordersCollection = _mongoDbContext.GetCollection<Order>("Orders");
                await ordersCollection.ReplaceOneAsync(filter, order);
            }
        }
    }
}
