using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Infrasctructure.Data.MongoDB
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDbConnection");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("PedidosDB");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
