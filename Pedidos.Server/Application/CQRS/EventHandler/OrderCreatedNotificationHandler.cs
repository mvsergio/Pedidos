using MediatR;
using MongoDB.Driver;
using Pedidos.Server.Application.CQRS.Notification;
using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application.CQRS.EventHandler
{
    public class OrderCreatedNotificationHandler : INotificationHandler<OrderCreatedNotification>
    {
        private readonly IMongoCollection<Order> _mongoCollection;

        public OrderCreatedNotificationHandler(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("OrdersDB");
            _mongoCollection = database.GetCollection<Order>("Orders");
        }

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

            await _mongoCollection.InsertOneAsync(order, cancellationToken: cancellationToken);

            Console.WriteLine($"Order {notification.OrderId} synchronized with MongoDB.");
        }
    }
}
