using Catalog.Api.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Api.Infrastructure
{
    public class FixedCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<Currency> _currencies =
            new[]
            {
                new Currency
                {
                    CurrencyCode = CurrencyCode.Euro,
                    DecimalPlaces = 2,
                    InUse = true
                },
                new Currency
                {
                    CurrencyCode = CurrencyCode.Usd,
                    DecimalPlaces = 2,
                    InUse = true
                }
            };

        public Currency FindCurrency(CurrencyCode currencyCode)
        {
            var currency = _currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode);
            return currency ?? Currency.None;
        }
    }
}