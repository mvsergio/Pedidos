using Pedidos.Server.Application.CQRS.SQL.Queries;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.SQL.Handlers
{
    public class GetOrderByIdQueryHandler
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> Handle(GetOrderByIdQuery query)
        {
            return await _orderRepository.GetByIdAsync(query.OrderId);
        }
    }
}
