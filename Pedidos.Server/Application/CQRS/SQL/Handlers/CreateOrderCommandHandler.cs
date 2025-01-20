using MediatR;
using Microsoft.EntityFrameworkCore;
using Pedidos.Server.Application.CQRS.Notification;
using Pedidos.Server.Application.CQRS.SQL.Commands;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.SQL.Handlers
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IMediator mediator) : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMediator _mediator = mediator;

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = request.Order.Id,
                CustomerId = request.Order.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                Items = []
            };

            decimal totalAmount = 0;

            foreach (var item in request.Order.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId) ?? throw new Exception("Produto não encontrado");
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                totalAmount += orderItem.TotalPrice;
                order.Items.Add(orderItem);
            }
            order.TotalAmount = totalAmount;

            await _orderRepository.CreateAsync(order);

            await _mediator.Publish(new OrderCreatedNotification
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            }, cancellationToken);

            return order;
        }
    }
}
