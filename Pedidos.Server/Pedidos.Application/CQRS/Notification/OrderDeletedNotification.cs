using MediatR;

namespace Pedidos.Application.CQRS.Notification
{
    public class OrderDeletedNotification : INotification
    {
        public int OrderId { get; set; }
    }
}
