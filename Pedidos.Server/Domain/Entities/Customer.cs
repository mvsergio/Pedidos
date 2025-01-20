﻿namespace Pedidos.Server.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; } 
        public string? Phone { get; set; }
        public List<Order> Orders { get; set; } = [];
    }
}
