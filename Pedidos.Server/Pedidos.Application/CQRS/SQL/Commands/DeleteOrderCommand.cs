using MediatR;

namespace Pedidos.Application.CQRS.SQL.Commands
{
    public record DeleteOrderCommand(int Id) : IRequest<bool>;
}

