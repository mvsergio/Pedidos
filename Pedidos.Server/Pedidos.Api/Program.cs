using MediatR;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using Pedidos.Api.Extensions;
using Pedidos.Application.CQRS.SQL.Commands;
using Pedidos.Application.CQRS.SQL.Queries;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Interfaces;

namespace Pedidos.Api
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.ResolveDependencies();

			var app = builder.Build();

			ConfigureMiddleware(app);
			ConfigureEndpoints(app);

			app.Run();
		}

		private static void ConfigureMiddleware(WebApplication app)
		{
			app.UseDefaultFiles();
			app.UseStaticFiles();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.MigrateDatabase();
			app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();
            app.MapControllers();
        }

        private static void ConfigureEndpoints(WebApplication app)
        {
            app.MapGet("/api/customers", () =>
            {
                var customers = new List<Customer>();
                var customer = new Customer()   
                {
                    Id = 1,
                    Name = "Cliente 1",
                    Email = "cliente1@desafio.com",
                    Phone = "123-456-7890"
                };
                customers.Add(customer);

                return Results.Ok(customers);
            });

            app.MapGet("/api/products", async (IMediator mediator) =>
            {
                var products = await mediator.Send(new GetProductsQuery());
                return products != null ? Results.Ok(products) : Results.NotFound();
            }).WithName("GetProducts").WithOpenApi();

            app.MapPost("/api/products", async (Product newProduct, IMediator mediator) =>
            {
                var createdProduct = await mediator.Send(new CreateProductCommand(newProduct));
                return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
            }).WithName("CreateProduct")
            .WithOpenApi();

            app.MapGet("/api/products/{id:int}", async (int id, IMediator mediator) =>
            {
                var product = await mediator.Send(new GetProductByIdQuery(id));
                return product is not null ? Results.Ok(product) : Results.NotFound();
            }).WithName("GetProductById")
            .WithOpenApi();

            app.MapGet("/api/orders", async (IMediator mediator) =>
            {
                var orders = await mediator.Send(new GetOrdersQuery());
                return Results.Ok(orders);
            });

            app.MapPost("/api/orders", async (Order newOrder, IMediator mediator) =>
            {
                var createdOrder = await mediator.Send(new CreateOrderCommand(newOrder));
                return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
            });

            app.MapGet("/api/orders/{id:int}", async (int id, IMediator mediator) =>
            {
                var order = await mediator.Send(new GetOrderByIdQuery(id));
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });

            app.MapFallbackToFile("/index.html");
        }
    }
}