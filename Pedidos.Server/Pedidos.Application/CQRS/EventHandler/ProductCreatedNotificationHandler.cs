using MediatR;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Application.CQRS.EventHandler
{
	public class ProductCreatedNotificationHandler(IMongoProductRepository mongoProductRepository) : INotificationHandler<ProductCreatedNotification>
    {
        public async Task Handle(ProductCreatedNotification notification, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = notification.ProcuctId,
                Name = notification.Name,
                Price = notification.Price
            };
            await mongoProductRepository.SaveProductAsync(product);
        }
    }
}
