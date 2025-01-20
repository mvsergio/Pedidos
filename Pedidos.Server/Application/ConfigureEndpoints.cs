using Pedidos.Server.Application.CQRS.NoSQL.Commands;
using Pedidos.Server.Application.CQRS.NoSQL.Handles;
using Pedidos.Server.Application.CQRS.NoSQL.Queries;
using Pedidos.Server.Application.CQRS.SQL.Commands;
using Pedidos.Server.Application.CQRS.SQL.Handlers;
using Pedidos.Server.Application.CQRS.SQL.Queries;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server.Application
{
    public class ConfigureEndpoints
    {
        private readonly WebApplication _app;

        public ConfigureEndpoints(WebApplication app)
        {
            _app = app;
        }

        public void Configure()
        {
            ConfigureCustomers();
            ConfigureProducts();
            ConfigureOrders();
        }

        private void ConfigureCustomers()
        {
            // Customer Endpoints
            _app.MapGet("/api/customers", () =>
            {
                var customer = new
                {
                    Id = 1,
                    Name = "Default Customer",
                    Email = "customer@example.com",
                    Phone = "123-456-7890"
                };
                return Results.Ok(customer);
            });
        }
        private void ConfigureProducts()
        {
            // Get all products
            _app.MapGet("/api/products", async (IProductRepository productRepository) =>
            {
                var products = await productRepository.GetAllAsync();
                return Results.Ok(products);
            });

            // Get a product by ID
            _app.MapGet("/api/products/{id:int}", async (int id, IProductRepository productRepository) =>
            {
                var product = await productRepository.GetByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            // Create a new product
            _app.MapPost("/api/products", async (Product newProduct, IProductRepository productRepository) =>
            {
                var createdProduct = await productRepository.CreateAsync(newProduct);
                return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
            });
        }

        private void ConfigureOrders()
        {
            // Get all orders (relational DB)
            _app.MapGet("/api/orders", async (GetAllOrdersQueryHandler handler) =>
            {
                var query = new GetAllOrdersQuery();
                var orders = await handler.Handle(query);
                return Results.Ok(orders);
            });

            // Get a specific order by ID (relational DB)
            _app.MapGet("/api/orders/{id:int}", async (int id, GetOrderByIdQueryHandler handler) =>
            {
                var query = new GetOrderByIdQuery(id);
                var order = await handler.Handle(query);
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });

            // Create a new order (relational DB)
            _app.MapPost("/api/orders", async (CreateOrderCommand command, CreateOrderCommandHandler handler) =>
            {
                var createdOrder = await handler.Handle(command);
                return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
            });


            // Get all orders (read model from MongoDB)
            _app.MapGet("/api/mongo/orders", async (GetOrdersFromMongoQueryHandler handler) =>
            {
                var query = new GetOrdersFromMongoQuery();
                var orders = await handler.Handle(query);
                return Results.Ok(orders);
            });


            // Get a specific order by ID (read model from MongoDB)
            _app.MapGet("/api/mongo/orders/{id}", async (int id, GetOrderByIdFromMongoQueryHandler handler) =>
            {
                var query = new GetOrderByIdFromMongoQuery(id);
                var order = await handler.Handle(query);
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });

            // Create a new order (MongoDB)
            _app.MapPost("/api/mongo/orders/sync", async (SyncOrderToMongoCommand command, SyncOrderToMongoCommandHandler handler) =>
            {
                await handler.Handle(command);
                return Results.Ok("Order synchronized to MongoDB.");
            });
        }

    }
}
