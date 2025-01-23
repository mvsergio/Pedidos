using Microsoft.EntityFrameworkCore;
using Pedidos.Infrasctructure.Data;

namespace Pedidos.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }
}