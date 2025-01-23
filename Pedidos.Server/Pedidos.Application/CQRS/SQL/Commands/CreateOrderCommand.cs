using MediatR;
using Pedidos.Domain.Entities;

namespace Pedidos.Application.CQRS.SQL.Commands
{
	public record CreateOrderCommand(Order Order) : IRequest<Order>;
}
