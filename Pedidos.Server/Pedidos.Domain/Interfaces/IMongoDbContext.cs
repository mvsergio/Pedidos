using MongoDB.Driver;

namespace Pedidos.Domain.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
