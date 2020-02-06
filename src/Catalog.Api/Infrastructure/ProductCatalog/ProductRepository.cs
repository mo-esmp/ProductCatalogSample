using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
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

        public void EditProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public Task<bool> CheckProductExistByCodeAsync(string code)
        {
            return _context.Products.AnyAsync(p => p.Code.Value == code);
        }

        public ValueTask<Product> GetProductByIdAsync(Guid id)
        {
            return _context.Products.FindAsync(id);
        }
    }
}