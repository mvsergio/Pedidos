using MediatR;
using Pedidos.Domain.Entities;

namespace Pedidos.Application.CQRS.SQL.Commands
{
	public record CreateOrderItemCommand(OrderItem BlogRequest) : IRequest<OrderItem>;
}
