namespace Pedidos.Server.Application.CQRS.SQL.Queries
{
    public class GetOrderByIdQuery(int orderId)
    {
        public int OrderId { get; set; } = orderId;
    }
}
