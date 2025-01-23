using MediatR;

namespace Pedidos.Application.CQRS.SQL.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<bool>;
}
