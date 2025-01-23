using MediatR;

namespace Pedidos.Application.CQRS.Notification
{
    public class ProductCreatedNotification : INotification
    {
        public int ProcuctId { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
