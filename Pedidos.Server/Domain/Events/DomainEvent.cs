﻿namespace Pedidos.Server.Domain.Events
{
    public class DomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}