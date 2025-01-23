using MediatR;
using Pedidos.Application.CQRS.SQL.Queries;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers;

public class GetOrdersQueryHandler(IMongoOrderRepository repository) : IRequestHandler<GetOrdersQuery, List<Order>>
{
    public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetOrdersAsync();
    }
}