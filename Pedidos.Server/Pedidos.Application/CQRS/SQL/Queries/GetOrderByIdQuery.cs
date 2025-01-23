using MediatR;
using Pedidos.Domain.Entities;

namespace Pedidos.Application.CQRS.SQL.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<Order>;
}
