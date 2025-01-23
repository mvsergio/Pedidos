using MediatR;
using Pedidos.Domain.Entities;

namespace Pedidos.Application.CQRS.SQL.Commands
{
	public record CreateProductCommand(Product request) : IRequest<Product>;
}
