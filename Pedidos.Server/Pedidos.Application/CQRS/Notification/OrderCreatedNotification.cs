using MediatR;

namespace Pedidos.Application.CQRS.Notification
{
    public class OrderCreatedNotification : INotification
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; }
    }
}
