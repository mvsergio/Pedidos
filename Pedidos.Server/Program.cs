using MediatR;
using Microsoft.EntityFrameworkCore;
using Pedidos.Server.Application.CQRS.EventHandler;
using Pedidos.Server.Application.CQRS.SQL.Handlers;
using Pedidos.Server.Application.CQRS.SQL.Queries;
using Pedidos.Server.Application.Service;
using Pedidos.Server.Domain.Entities;
using Pedidos.Server.Infra.Data;
using Pedidos.Server.Infra.Data.MongoDB;
using Pedidos.Server.Infra.Repositories.MongoDB;
using Pedidos.Server.Infra.Repositories.SqlServer;

namespace Pedidos.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // MongoDB
            builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
            builder.Services.AddScoped<IMongoOrderRepository, MongoOrderRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

            // CQRS 
            builder.Services.AddScoped<CreateOrderCommandHandler>();
            builder.Services.AddScoped<GetAllOrdersQueryHandler>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<OrderCreatedNotificationHandler>();
            builder.Services.AddScoped<OrderDeleteddNotificationHandler>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // app.UseAuthorization();

            // endpoints
            app.MapGet("/api/customers", () =>
            {
                var customer = new
                {
                    Id = 1,
                    Name = "Cliente 1",
                    Email = "cliente1@desafio.com",
                    Phone = "123-456-7890"
                };
                return Results.Ok(customer);
            });

            app.MapGet("/api/products", async (IMediator _mediator) =>
            {
                var products = await _mediator.Send(new GetProductsQuery());
                return products != null ? Results.Ok(products) : Results.NotFound();
            }).WithName("GetProducts")
            .WithOpenApi();


            app.MapPost("/api/products", async (Product newProduct, IProductService productService) =>
            {
                var createdProduct = await productService.CreateProductAsync(newProduct);
                return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
            }).WithName("CreateProduct")
            .WithOpenApi();

            app.MapGet("/api/products/{id:int}", async (int id, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            }).WithName("GetProductById")
            .WithOpenApi();


            app.MapGet("/api/orders", async (IOrderService orderService) =>
            {
                var orders = await orderService.GetAllOrdersAsync();
                return Results.Ok(orders);
            });

            app.MapPost("/api/orders", async (Order newOrder, IOrderService orderService) =>
            {
                var createdOrder = await orderService.CreateOrderAsync(newOrder);
                return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
            });

            app.MapGet("/api/orders/{id:int}", async (int id, IOrderService orderService) =>
            {
                var order = await orderService.GetOrderByIdAsync(id);
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
