using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Infra.Repositories.MongoDB
{
	public class MongoOrderRepository(IMongoDbContext mongoDbContext) : IMongoOrderRepository
    {
        private readonly IMongoCollection<Order> _orderCollection = mongoDbContext.GetCollection<Order>("Orders");

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

        public async Task DeleteOrderAsync(int id)
        {
            var existingOrder = await _orderCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (existingOrder != null)
                await _orderCollection.DeleteOneAsync(o => o.Id == id);
        }
    }
}
