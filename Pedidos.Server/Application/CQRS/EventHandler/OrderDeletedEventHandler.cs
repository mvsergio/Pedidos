using MongoDB.Driver;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Domain.Events;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Application.CQRS.EventHandler
{
    public class OrderDeletedEventHandler(IMongoDbContext mongoDbContext)
    {
        private readonly IMongoDbContext _mongoDbContext = mongoDbContext;

        public async Task Handle(OrderDeletedEvent orderDeletedEvent)
        {
            // Remove the order from MongoDB
            var ordersCollection = _mongoDbContext.GetCollection<Order>("Orders");
            await ordersCollection.DeleteOneAsync(o => o.Id == orderDeletedEvent.OrderId);
        }
    }
}
