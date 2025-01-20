using MongoDB.Driver;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;

namespace Pedidos.Server.Infra.Repositories.MongoDB
{
    public class MongoOrderRepository : IMongoOrderRepository
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public MongoOrderRepository(IMongoDbContext mongoDbContext)
        {
            _orderCollection = mongoDbContext.GetCollection<Order>("Orders");
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _orderCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveOrderAsync(Order order)
        {
            var existingOrder = await _orderCollection.Find(o => o.Id == order.Id).FirstOrDefaultAsync();
            if (existingOrder == null)
            {
                await _orderCollection.InsertOneAsync(order);
            }
            else
            {
                await _orderCollection.ReplaceOneAsync(o => o.Id == order.Id, order);
            }
        }
    }
}
