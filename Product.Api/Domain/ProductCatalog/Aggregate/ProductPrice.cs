using Product.Api.Domain.Exceptions;
using Product.Api.Domain.Shared;

namespace Product.Api.Domain.ProductCatalog
{
    public class ProductPrice : Money
    {
        public new static ProductPrice FromMoney(Money money)
        {
            if (money.Amount <= 0)
                throw new InvalidPriceDomainException("Product price must be higher than 0");

            return new ProductPrice { Amount = money.Amount, Currency = money.Currency };
        }
    }
}