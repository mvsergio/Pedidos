using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.EventHandler
{
	public class OrderDeletedNotificationHandler(IMongoOrderRepository mongoOrderRepository) : INotificationHandler<OrderDeletedNotification>
    {
        private readonly IMongoOrderRepository _mongoOrderRepository = mongoOrderRepository;

        public async Task Handle(OrderDeletedNotification notification, CancellationToken cancellationToken)
        {
            await _mongoOrderRepository.DeleteOrderAsync(notification.OrderId);
        }
    }
}
