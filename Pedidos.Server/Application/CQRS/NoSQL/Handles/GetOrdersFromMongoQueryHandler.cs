using MongoDB.Driver;
using Pedidos.Server.Application.CQRS.NoSQL.Queries;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Application.CQRS.NoSQL.Handles
{
    public class GetOrdersFromMongoQueryHandler
    {
        private readonly IMongoDbContext _mongoDbContext;

        public GetOrdersFromMongoQueryHandler(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<List<Order>> Handle(GetOrdersFromMongoQuery query)
        {
            var ordersCollection = _mongoDbContext.GetCollection<Order>("Orders");
            return await ordersCollection.Find(_ => true).ToListAsync();
        }
    }
}
