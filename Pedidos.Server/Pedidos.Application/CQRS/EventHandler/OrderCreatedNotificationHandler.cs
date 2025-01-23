using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.EventHandler
{
	public class OrderCreatedNotificationHandler(IMongoOrderRepository mongoOrderRepository) : INotificationHandler<OrderCreatedNotification>
    {
        private readonly IMongoOrderRepository _mongoOrderRepository = mongoOrderRepository;

        public async Task Handle(OrderCreatedNotification notification, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = notification.OrderId,
                CustomerId = notification.CustomerId,
                TotalAmount = notification.TotalAmount,
                Status = notification.Status,
                OrderDate = DateTime.UtcNow
            };
            await _mongoOrderRepository.SaveOrderAsync(order);
        }
    }
}
