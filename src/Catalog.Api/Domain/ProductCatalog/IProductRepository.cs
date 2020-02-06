using System;
using System.Threading.Tasks;

namespace Catalog.Api.Domain.ProductCatalog
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        void EditProduct(Product product);

        void RemoveProduct(Product product);

        Task<bool> CheckProductExistByCodeAsync(string code);

        ValueTask<Product> GetProductByIdAsync(Guid id);
    }
}