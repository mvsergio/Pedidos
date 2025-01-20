namespace Pedidos.Server.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required string Status { get; set; } // Enum as string for simplicity
        public List<OrderItem> Items { get; set; } = [];
    }
}
