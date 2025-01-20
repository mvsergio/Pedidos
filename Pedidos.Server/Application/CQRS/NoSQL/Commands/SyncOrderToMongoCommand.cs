using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application.CQRS.NoSQL.Commands
{
    public class SyncOrderToMongoCommand
    {
        public Order Order { get; set; }

        public SyncOrderToMongoCommand(Order order)
        {
            Order = order;
        }
    }
}
