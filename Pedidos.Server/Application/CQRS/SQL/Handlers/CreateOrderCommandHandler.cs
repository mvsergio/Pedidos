using Pedidos.Server.Application.CQRS.SQL.Commands;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.SQL.Handlers
{
    public class CreateOrderCommandHandler
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand command)
        {
            var order = new Order
            {
                CustomerId = command.CustomerId,
                OrderDate = DateTime.UtcNow,
                Items = command.Items,
                TotalAmount = command.Items.Sum(item => item.TotalPrice),
                Status = "Pending"
            };

            return await _orderRepository.CreateAsync(order);
        }
    }
}
