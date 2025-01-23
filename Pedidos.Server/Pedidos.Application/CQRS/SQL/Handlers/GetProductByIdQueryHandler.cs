using MediatR;
using Pedidos.Application.CQRS.SQL.Queries;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers;
public class GetProductByIdQueryHandler(IMongoProductRepository repository) : IRequestHandler<GetProductByIdQuery, Product?>
{
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetProductByIdAsync(request.Id);
    }
}