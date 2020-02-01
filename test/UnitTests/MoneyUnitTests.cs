using Moq;
using Product.Api.Domain.Shared;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class MoneyUnitTests
    {
        private readonly ICurrencyLookup _currencyLookup;

        public MoneyUnitTests()
        {
            _currencyLookup = CreateFakeLookup();
        }

        [Fact]
        public void Two_Of_Same_Currency_And_Amount_Should_Be_Equal()
        {
            // Arrange
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, _currencyLookup);
            var secondAmount = Money.FromDecimal(5, CurrencyCode.Euro, _currencyLookup);
            // Act

            // Asset
            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_Of_Same_Currency_But_Different_Amount_Should_Not_Be_Equal()
        {
            // Arrange
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, _currencyLookup);
            var secondAmount = Money.FromDecimal(4, CurrencyCode.Euro, _currencyLookup);

            // Act

            // Asset
            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_Of_Same_Different_Currency_But_Same_Amount_Should_Not_Be_Equal()
        {
            // Arrange
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, _currencyLookup);
            var secondAmount = Money.FromDecimal(5, CurrencyCode.Usd, _currencyLookup);

            // Act

            // Asset
            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_Of_Money_Should_Be_Equal_With_Total_Amount()
        {
            // Arrange
            var money1 = Money.FromDecimal(1, CurrencyCode.Euro, _currencyLookup);
            var money2 = Money.FromDecimal(2, CurrencyCode.Euro, _currencyLookup);
            var totalMoney = Money.FromDecimal(3, CurrencyCode.Euro, _currencyLookup);

            // Act

            // Asset
            Assert.Equal(totalMoney, money1 + money2);
        }

        [Fact]
        public void Subtract_Of_Money_Should_Be_Equal_With_Total_Amount()
        {
            // Arrange
            var money1 = Money.FromDecimal(4, CurrencyCode.Euro, _currencyLookup);
            var money2 = Money.FromDecimal(1, CurrencyCode.Euro, _currencyLookup);
            var totalMoney = Money.FromDecimal(3, CurrencyCode.Euro, _currencyLookup);

            // Act

            // Asset
            Assert.Equal(totalMoney, money1 - money2);
        }

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