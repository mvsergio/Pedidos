using Pedidos.Server.Application.CQRS.NoSQL.Commands;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;
using Pedidos.Server.Infra.Repositories.MongoDB;

namespace Pedidos.Server.Application.CQRS.NoSQL.Handles
{
    public class SyncOrderToMongoCommandHandler
    {
        private readonly IMongoOrderRepository _mongoOrderRepository;
        public SyncOrderToMongoCommandHandler(IMongoOrderRepository mongoOrderRepository)
        {
            _mongoOrderRepository = mongoOrderRepository;
        }

        public async Task Handle(SyncOrderToMongoCommand command)
        {
            await _mongoOrderRepository.SaveOrderAsync(command.Order);
        }
    }
}
