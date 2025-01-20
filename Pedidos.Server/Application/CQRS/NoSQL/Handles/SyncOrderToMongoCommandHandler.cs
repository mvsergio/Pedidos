using Pedidos.Server.Application.CQRS.NoSQL.Commands;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Application.CQRS.NoSQL.Handles
{
    public class SyncOrderToMongoCommandHandler
    {
        private readonly IMongoDbContext _mongoDbContext;

        public SyncOrderToMongoCommandHandler(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task Handle(SyncOrderToMongoCommand command)
        {
            var ordersCollection = _mongoDbContext.GetCollection<Order>("Orders");
            await ordersCollection.InsertOneAsync(command.Order);
        }
    }
}
