using MediatR;

namespace Pedidos.Server.Application.CQRS.SQL.Commands
{
    public record DeleteOrderCommand(int Id) : IRequest<bool>;
}
