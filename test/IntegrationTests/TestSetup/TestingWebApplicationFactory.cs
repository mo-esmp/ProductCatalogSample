using Catalog.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace IntegrationTests.TestSetup
{
    public class TestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ProductDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ProductDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<TestingWebApplicationFactory<TStartup>>>();
                db.Database.EnsureCreated();

                try
                {
                    DbInitializer.InitializeDb(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }
}