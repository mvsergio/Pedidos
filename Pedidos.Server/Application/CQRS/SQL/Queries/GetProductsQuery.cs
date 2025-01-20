using MediatR;
using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application.CQRS.SQL.Queries
{
    public record GetProductsQuery() : IRequest<Product>;
}
