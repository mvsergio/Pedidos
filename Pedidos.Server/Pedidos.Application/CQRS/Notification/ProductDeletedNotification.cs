using MediatR;

namespace Pedidos.Application.CQRS.Notification;

public class ProductDeletedNotification : INotification
{
    public int ProductId { get; set; }
}