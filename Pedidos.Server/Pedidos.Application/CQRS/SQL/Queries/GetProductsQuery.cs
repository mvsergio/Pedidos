﻿using MediatR;
using Pedidos.Domain.Entities;

namespace Pedidos.Application.CQRS.SQL.Queries
{
	public record GetProductsQuery() : IRequest<List<Product>>;
}
