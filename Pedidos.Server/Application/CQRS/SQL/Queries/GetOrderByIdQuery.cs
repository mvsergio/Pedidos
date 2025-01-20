namespace Pedidos.Server.Application.CQRS.SQL.Queries
{
    public class GetOrderByIdQuery
    {
        public int OrderId { get; set; }

        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
