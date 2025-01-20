namespace Pedidos.Server.Domain.Events
{
    public class OrderDeletedEvent : DomainEvent
    {
        public int OrderId { get; }

        public OrderDeletedEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
