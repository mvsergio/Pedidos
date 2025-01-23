using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.EventHandler
{
	public class ProductDeletedNotificationHandler(IMongoProductRepository mongoProductRepository) : INotificationHandler<ProductDeletedNotification>
    {
        public async Task Handle(ProductDeletedNotification notification, CancellationToken cancellationToken)
        {
            await mongoProductRepository.DeleteProductAsync(notification.ProductId);
        }
    }
}
