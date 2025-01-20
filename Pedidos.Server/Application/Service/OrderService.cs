using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.MongoDB;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application.Service
{
    public class OrderService(IOrderRepository orderRepository, IMongoOrderRepository mongoOrderRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IMongoOrderRepository _mongoOrderRepository = mongoOrderRepository;

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Calcular total do pedido
            order.TotalAmount = order.Items.Sum(item => item.TotalPrice);

            // Adicionar data do pedido
            order.OrderDate = DateTime.UtcNow;

            // Adicionar validações e regras de negócio aqui

            var createdOrder = await _orderRepository.CreateAsync(order);

            // Salvar para o MongoDB (read model)
            await _mongoOrderRepository.SaveOrderAsync(createdOrder);

            return createdOrder;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);

            // Atualizar no MongoDB (read model)
            await _mongoOrderRepository.SaveOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);

            // Não há necessidade de remover do MongoDB explicitamente, 
            // dependendo da abordagem de sincronização.
        }
    }
}
