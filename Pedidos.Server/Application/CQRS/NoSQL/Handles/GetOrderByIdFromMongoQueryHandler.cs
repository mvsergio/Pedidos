using MongoDB.Driver;
using Pedidos.Server.Application.CQRS.NoSQL.Queries;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Application.CQRS.NoSQL.Handles
{
    public class GetOrderByIdFromMongoQueryHandler
    {
        private readonly IMongoDbContext _mongoDbContext;

        public GetOrderByIdFromMongoQueryHandler(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<Order?> Handle(GetOrderByIdFromMongoQuery query)
        {
            var ordersCollection = _mongoDbContext.GetCollection<Order>("Orders");
            return await ordersCollection.Find(o => o.Id == query.OrderId).FirstOrDefaultAsync();
        }
    }
}
