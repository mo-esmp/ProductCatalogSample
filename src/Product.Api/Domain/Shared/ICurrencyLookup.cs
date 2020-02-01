namespace Product.Api.Domain.Shared
{
    public interface ICurrencyLookup
    {
        Currency FindCurrency(CurrencyCode currencyCode);
    }
}