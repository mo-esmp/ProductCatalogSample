using Catalog.Api.Domain.Exceptions;
using Catalog.Api.Domain.Shared;
using Catalog.Api.Resources;

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
                throw new InvalidPriceDomainException(ErrorMessagesResource.InvalidProductPriceError);

            return new ProductPrice { Amount = money.Amount, Currency = money.Currency };
        }
    }
}