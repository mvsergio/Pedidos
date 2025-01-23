namespace Pedidos.Domain.Events
{
    public class OrderCreatedEvent : DomainEvent
    {
        public int OrderId { get; }

        public OrderCreatedEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
