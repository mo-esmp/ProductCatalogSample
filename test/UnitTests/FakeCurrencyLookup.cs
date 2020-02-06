using Catalog.Api.Domain.Shared;
using Moq;
using System.Linq;

namespace UnitTests
{
    internal class FakeCurrencyLookup
    {
        private static readonly ICurrencyLookup _lookup = CreateFakeLookup();

        public static ICurrencyLookup Lookup => _lookup;

        private static ICurrencyLookup CreateFakeLookup()
        {
            var currencies =
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
                    },
                };

            var mock = new Mock<ICurrencyLookup>();
            mock.Setup(cl => cl.FindCurrency(It.IsAny<CurrencyCode>()))
                .Returns((CurrencyCode currencyCode) =>
                {
                    return currencies.SingleOrDefault(c => c.CurrencyCode == currencyCode);
                });

            return mock.Object;
        }
    }
}