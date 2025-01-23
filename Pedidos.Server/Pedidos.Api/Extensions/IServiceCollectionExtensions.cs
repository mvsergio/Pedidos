using MediatR;
using Microsoft.EntityFrameworkCore;
using Pedidos.Application.CQRS.EventHandler;
using Pedidos.Application.CQRS.Notification;
using Pedidos.Application.CQRS.SQL.Commands;
using Pedidos.Application.CQRS.SQL.Handlers;
using Pedidos.Application.CQRS.SQL.Queries;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;
using Pedidos.Infra.Repositories.MongoDB;
using Pedidos.Infra.Repositories.SqlServer;
using Pedidos.Infrasctructure.Data;
using Pedidos.Infrasctructure.Data.MongoDB;

namespace Pedidos.Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Adquirindo o Configuration
        var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();

        // Contextos de Banco de Dados
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddSingleton<IMongoDbContext, MongoDbContext>();

        // Repositorios
        services.AddScoped<IOrdersRepository, OrderRepository>();
        services.AddScoped<IProductsRepository, ProductRepository>();
        services.AddScoped<IMongoOrderRepository, MongoOrderRepository>();
        services.AddScoped<IMongoProductRepository, MongoProductRepository>();

        // Handlers CQRS
        services.AddScoped<CreateOrderCommandHandler>();
        services.AddScoped<CreateProductCommandHandler>();
        services.AddScoped<GetOrdersQueryHandler>();
        services.AddScoped<GetProductsQueryHandler>();
        services.AddScoped<OrderCreatedNotificationHandler>();
        services.AddScoped<ProductCreatedNotificationHandler>();
        services.AddScoped<OrderDeletedNotificationHandler>();
        services.AddScoped<ProductDeletedNotificationHandler>();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

        // Controladores
        services.AddControllers();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Registro do IRequestHandlers
        services.AddTransient<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
        services.AddTransient<IRequestHandler<CreateProductCommand, Product>, CreateProductCommandHandler>();
        services.AddTransient<IRequestHandler<GetOrdersQuery, List<Order>>, GetOrdersQueryHandler>();
        services.AddTransient<IRequestHandler<GetProductsQuery, List<Product>>, GetProductsQueryHandler>();
        services.AddTransient<IRequestHandler<GetOrderByIdQuery, Order?>, GetOrderByIdQueryHandler>();
        services.AddTransient<IRequestHandler<GetProductByIdQuery, Product?>, GetProductByIdQueryHandler>();

        services.AddScoped<INotificationHandler<OrderCreatedNotification>, OrderCreatedNotificationHandler>();
        services.AddScoped<INotificationHandler<ProductCreatedNotification>, ProductCreatedNotificationHandler>();

        return services;
    }
}
