namespace Pedidos.Server.Domain.Events
{
    public class OrderUpdatedEvent : DomainEvent
    {
        public int OrderId { get; }

        public OrderUpdatedEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
