using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;

namespace Pedidos.Infrasctructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasKey(p => p.Id);
			modelBuilder.Entity<Order>().HasKey(o => o.Id);
			modelBuilder.Entity<OrderItem>().HasKey(oi => oi.Id);

			modelBuilder.Entity<OrderItem>()
				.HasOne<Order>()
				.WithMany(o => o.Items)
				.HasForeignKey(oi => oi.OrderId);

			// Seed data for Products
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Product A", Price = 10.0M },
				new Product { Id = 2, Name = "Product B", Price = 20.0M },
				new Product { Id = 3, Name = "Product C", Price = 30.0M }
			);
		}
	}
}
