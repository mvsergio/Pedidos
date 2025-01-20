using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application.CQRS.SQL.Commands
{
    public class CreateOrderCommand
    {
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
