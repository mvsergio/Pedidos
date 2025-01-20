using MongoDB.Driver;

namespace Pedidos.Server.Infra.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
