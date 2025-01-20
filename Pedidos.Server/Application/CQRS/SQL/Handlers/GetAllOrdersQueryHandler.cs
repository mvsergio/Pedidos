using Pedidos.Server.Application.CQRS.SQL.Queries;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.CQRS.SQL.Handlers
{
    public class GetAllOrdersQueryHandler
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(GetAllOrdersQuery query)
        {
            return await _orderRepository.GetAllAsync();
        }
    }
}
