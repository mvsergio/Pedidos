namespace Pedidos.Server.Application.CQRS.NoSQL.Queries
{
    public class GetOrderByIdFromMongoQuery
    {
        public int OrderId { get; set; }

        public GetOrderByIdFromMongoQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
