using Catalog.Api.Infrastructure.Data;

namespace IntegrationTests.TestSetup
{
    internal static class DbInitializer
    {
        public static void InitializeDb(ProductDbContext context)
        {
            foreach (var product in InitialProductList.Products)
                context.Products.Add(product);

            context.SaveChanges();
        }
    }
}