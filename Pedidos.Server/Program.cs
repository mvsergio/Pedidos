using Microsoft.EntityFrameworkCore;
using Pedidos.Server.Application.CQRS.EventHandler;
using Pedidos.Server.Application.CQRS.NoSQL.Handles;
using Pedidos.Server.Application.CQRS.SQL.Handlers;
using Pedidos.Server.Application.Service;
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

            // CQRS - sql
            builder.Services.AddScoped<CreateOrderCommandHandler>();
            builder.Services.AddScoped<GetOrderByIdQueryHandler>();
            builder.Services.AddScoped<GetAllOrdersQueryHandler>();

            // CQRS - nosql
            builder.Services.AddScoped<SyncOrderToMongoCommandHandler>();
            builder.Services.AddScoped<GetOrdersFromMongoQueryHandler>();
            builder.Services.AddScoped<GetOrderByIdFromMongoQueryHandler>();

            // Event Handler CQRS Sql X NoSQL
            builder.Services.AddScoped<OrderCreatedEventHandler>();
            builder.Services.AddScoped<OrderUpdatedEventHandler>();
            builder.Services.AddScoped<OrderDeletedEventHandler>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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

            app.UseAuthorization();

            //var summaries = new[]
            //{
            //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            //};

            //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            //{
            //    var forecast = Enumerable.Range(1, 5).Select(index =>
            //        new WeatherForecast
            //        {
            //            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //            TemperatureC = Random.Shared.Next(-20, 55),
            //            Summary = summaries[Random.Shared.Next(summaries.Length)]
            //        })
            //        .ToArray();
            //    return forecast;
            //})
            //.WithName("GetWeatherForecast")
            //.WithOpenApi();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
