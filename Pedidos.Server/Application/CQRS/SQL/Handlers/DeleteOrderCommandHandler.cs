using MediatR;
using Pedidos.Server.Application.CQRS.Notification;
using Pedidos.Server.Application.CQRS.SQL.Commands;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.SQL.Handlers
{
    public class DeleteOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator) : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMediator _mediator = mediator;

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
                return false;

            await _orderRepository.DeleteAsync(order.Id);

            await _mediator.Publish(new OrderDeletedNotification
            {
                OrderId = order.Id
            }, cancellationToken);

            return true;
        }
    }

}
