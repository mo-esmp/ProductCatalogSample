using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Catalog.Api.Infrastructure.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                try
                {
                    var applied = dbContext.GetService<IHistoryRepository>()
                        .GetAppliedMigrations()
                        .Select(m => m.MigrationId);

                    var total = dbContext.GetService<IMigrationsAssembly>()
                        .Migrations.Select(m => m.Key);

                    var hasPendingMigration = total.Except(applied).Any();
                    if (hasPendingMigration)
                        dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return host;
        }
    }
}