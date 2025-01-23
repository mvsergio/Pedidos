using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Infra.Repositories.MongoDB
{
	public class MongoProductRepository(IMongoDbContext mongoDbContext) : IMongoProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection = mongoDbContext.GetCollection<Product>("Products");

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveProductAsync(Product product)
        {
            var existingProduct = await _productCollection.Find(o => o.Id == product.Id).FirstOrDefaultAsync();
            if (existingProduct == null)
                await _productCollection.InsertOneAsync(product);
            else
                await _productCollection.ReplaceOneAsync(o => o.Id == product.Id, product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (product != null)
                await _productCollection.DeleteOneAsync(o => o.Id == id);
        }
    }
}
