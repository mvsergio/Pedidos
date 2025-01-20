using MediatR;
using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application.CQRS.SQL.Commands
{
    public record CreateProductCommand(Product BlogRequest) : IRequest<Product>;
}
