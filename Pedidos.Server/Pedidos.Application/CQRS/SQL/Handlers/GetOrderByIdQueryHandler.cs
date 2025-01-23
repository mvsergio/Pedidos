using MediatR;
using Pedidos.Application.CQRS.SQL.Queries;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers;
public class GetOrderByIdQueryHandler(IMongoOrderRepository repository) : IRequestHandler<GetOrderByIdQuery, Order?>
{
    public Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return repository.GetOrderByIdAsync(request.Id);
    }
}
