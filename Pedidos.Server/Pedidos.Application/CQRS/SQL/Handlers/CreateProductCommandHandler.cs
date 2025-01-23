using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Application.CQRS.SQL.Commands;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers;
public class CreateProductCommandHandler(IProductsRepository repository, IMediator mediator) : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var exitingProduct = await repository.GetByIdAsync(command.request.Id);
        if (exitingProduct is not null)
            throw new Exception("Produto já existente.");

        await repository.CreateAsync(command.request);

        await mediator.Publish(new ProductCreatedNotification
        {
            ProcuctId = command.request.Id,
            Name = command.request.Name,
            Price = command.request.Price
        }, cancellationToken);

        return command.request;
    }
}