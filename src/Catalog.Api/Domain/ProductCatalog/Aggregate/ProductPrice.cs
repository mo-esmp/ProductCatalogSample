using Catalog.Api.Domain.Exceptions;
using Catalog.Api.Domain.Shared;

namespace Catalog.Api.Domain.ProductCatalog
{
    public class ProductPrice : Money
    {
        protected ProductPrice()
        {
        }

        public static ProductPrice FromMoney(Money money)
        {
            if (money.Amount <= 0)
                throw new InvalidPriceDomainException("Product price must be higher than 0");

            return new ProductPrice { Amount = money.Amount, Currency = money.Currency };
        }
    }
}