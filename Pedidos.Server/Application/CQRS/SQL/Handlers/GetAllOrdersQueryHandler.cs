using Pedidos.Server.Application.CQRS.SQL.Queries;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.SQL.Handlers
{
    public class GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<List<Order>> Handle()
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
