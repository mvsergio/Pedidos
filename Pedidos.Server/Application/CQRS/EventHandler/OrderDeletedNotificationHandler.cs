using MediatR;
using MongoDB.Driver;
using Pedidos.Server.Application.CQRS.Notification;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.MongoDB;

namespace Pedidos.Server.Application.CQRS.EventHandler
{
    public class OrderDeleteddNotificationHandler(IMongoOrderRepository mongoOrderRepository) : INotificationHandler<OrderDeletedNotification>
    {
        private readonly IMongoOrderRepository _mongoOrderRepository = mongoOrderRepository;

        public async Task Handle(OrderDeletedNotification notification, CancellationToken cancellationToken)
        {
            await _mongoOrderRepository.DeleteOrderAsync(notification.OrderId);
        }
    }
}
