using MediatR;
using Pedidos.Domain.Entities;

namespace Pedidos.Application.CQRS.SQL.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}