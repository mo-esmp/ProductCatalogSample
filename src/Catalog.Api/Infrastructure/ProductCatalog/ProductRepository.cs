using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure.ProductCatalog
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public Task<bool> CheckProductExistByCodeAsync(string code)
        {
            return _context.Products.AnyAsync(p => p.Code.Value == code);
        }
    }
}