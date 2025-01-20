using MediatR;
using Pedidos.Server.Application.CQRS.SQL.Queries;
using Pedidos.Server.Application.Service;
using Pedidos.Server.Domain.Entities;

namespace Pedidos.Server.Application
{
    public class ConfigureEndpoints(WebApplication app, IMediator mediator)
    {
        private readonly WebApplication _app = app;
        private readonly IMediator _mediator = mediator;

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
            _app.MapGet("/api/products", async () =>
            {
                var products = await _mediator.Send(new GetProductsQuery());
                return products != null ? Results.Ok(products) : Results.NotFound();
            });

            _app.MapPost("/api/products", async (Product newProduct, IProductService productService) =>
            {
                var createdProduct = await productService.CreateProductAsync(newProduct);
                return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
            });

            _app.MapGet("/api/products/{id:int}", async (int id, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            //// Get all products
            //_app.MapGet("/api/products", async (IProductRepository productRepository) =>
            //{
            //    var products = await productRepository.GetAllAsync();
            //    return Results.Ok(products);
            //});

            //// Get a product by ID
            //_app.MapGet("/api/products/{id:int}", async (int id, IProductRepository productRepository) =>
            //{
            //    var product = await productRepository.GetByIdAsync(id);
            //    return product is not null ? Results.Ok(product) : Results.NotFound();
            //});

            //// Create a new product
            //_app.MapPost("/api/products", async (Product newProduct, IProductRepository productRepository) =>
            //{
            //    var createdProduct = await productRepository.CreateAsync(newProduct);
            //    return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
            //});
        }

        private void ConfigureOrders()
        {
            _app.MapGet("/api/orders", async (IOrderService orderService) =>
            {
                var orders = await orderService.GetAllOrdersAsync();
                return Results.Ok(orders);
            });

            _app.MapPost("/api/orders", async (Order newOrder, IOrderService orderService) =>
            {
                var createdOrder = await orderService.CreateOrderAsync(newOrder);
                return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
            });

            _app.MapGet("/api/orders/{id:int}", async (int id, IOrderService orderService) =>
            {
                var order = await orderService.GetOrderByIdAsync(id);
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });

            //// Create a new order (relational DB)
            //_app.MapPost("/api/orders", async (CreateOrderCommand command, IMediator mediator) =>
            //{
            //    var orderId = await mediator.Send(command);
            //    return Results.Created($"/api/orders/{orderId}", orderId);
            //});

            //// Get all orders (relational DB)
            //_app.MapGet("/api/orders", async (GetAllOrdersQueryHandler handler) =>
            //{
            //    var query = new GetAllOrdersQuery();
            //    var orders = await handler.Handle(query);
            //    return Results.Ok(orders);
            //});

            //// Get a specific order by ID (relational DB)
            //_app.MapGet("/api/orders/{id:int}", async (int id, IMediator mediator) =>
            //{
            //    var query = new GetOrderByIdQuery(id);
            //    var order = await mediator.Send(query);
            //    return order is not null ? Results.Ok(order) : Results.NotFound();
            //});

            //// Get all orders (read model from MongoDB)
            //_app.MapGet("/api/mongo/orders", async (GetOrdersFromMongoQueryHandler handler) =>
            //{
            //    var query = new GetOrdersFromMongoQuery();
            //    var orders = await handler.Handle(query);
            //    return Results.Ok(orders);
            //});


            //// Get a specific order by ID (read model from MongoDB)
            //_app.MapGet("/api/mongo/orders/{id}", async (int id, GetOrderByIdFromMongoQueryHandler handler) =>
            //{
            //    var query = new GetOrderByIdFromMongoQuery(id);
            //    var order = await handler.Handle(query);
            //    return order is not null ? Results.Ok(order) : Results.NotFound();
            //});

            //// Create a new order (MongoDB)
            //_app.MapPost("/api/mongo/orders/sync", async (SyncOrderToMongoCommand command, SyncOrderToMongoCommandHandler handler) =>
            //{
            //    await handler.Handle(command);
            //    return Results.Ok("Order synchronized to MongoDB.");
            //});
        }

    }
}
