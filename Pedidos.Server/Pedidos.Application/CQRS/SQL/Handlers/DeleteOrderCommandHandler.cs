using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Application.CQRS.SQL.Commands;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.SQL.Handlers
{
	public class DeleteOrderCommandHandler(IOrdersRepository orderRepository, IMediator mediator) : IRequestHandler<DeleteOrderCommand, bool>
	{
		private readonly IOrdersRepository _orderRepository = orderRepository;
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