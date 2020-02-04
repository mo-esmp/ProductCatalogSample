using System.Threading.Tasks;

namespace Catalog.Api.Domain.ProductCatalog
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        Task<bool> CheckProductExistByCodeAsync(string code);
    }
}