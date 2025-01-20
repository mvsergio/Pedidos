using MediatR;
using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application.CQRS.SQL.Commands
{
    public record CreateOrderItemCommand(OrderItem BlogRequest) : IRequest<OrderItem>;
}
