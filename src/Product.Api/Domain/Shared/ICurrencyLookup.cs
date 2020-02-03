namespace Catalog.Api.Domain.Shared
{
    public interface ICurrencyLookup
    {
        Currency FindCurrency(CurrencyCode currencyCode);
    }
}