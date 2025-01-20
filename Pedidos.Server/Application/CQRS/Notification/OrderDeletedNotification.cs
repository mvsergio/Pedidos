using MediatR;

namespace Pedidos.Server.Application.CQRS.Notification
{
    public class OrderDeletedNotification : INotification
    {
        public int OrderId { get; set; }
    }
}
