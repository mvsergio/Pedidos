using MediatR;
using MongoDB.Driver;
using Pedidos.Server.Application.CQRS.Notification;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.MongoDB;

namespace Pedidos.Server.Application.CQRS.EventHandler
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
