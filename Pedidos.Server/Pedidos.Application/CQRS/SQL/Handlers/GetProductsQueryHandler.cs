using MediatR;
using Pedidos.Application.CQRS.SQL.Queries;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers;

public class GetProductsQueryHandler(IMongoProductRepository repository) : IRequestHandler<GetProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetProductsAsync();
    }
}