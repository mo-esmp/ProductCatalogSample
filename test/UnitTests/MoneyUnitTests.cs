using Catalog.Api.Domain.Shared;
using Xunit;

namespace UnitTests
{
    public class MoneyUnitTests
    {
        [Fact]
        public void Two_Of_Same_Currency_And_Amount_Should_Be_Equal()
        {
            // Arrange
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var secondAmount = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            // Act

            // Asset
            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_Of_Same_Currency_But_Different_Amount_Should_Not_Be_Equal()
        {
            // Arrange
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var secondAmount = Money.FromDecimal(4, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act

            // Asset
            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void Two_Of_Same_Different_Currency_But_Same_Amount_Should_Not_Be_Equal()
        {
            // Arrange
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var secondAmount = Money.FromDecimal(5, CurrencyCode.Usd, FakeCurrencyLookup.Lookup);

            // Act

            // Asset
            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void Sum_Of_Money_Should_Be_Equal_With_Total_Amount()
        {
            // Arrange
            var money1 = Money.FromDecimal(1, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var money2 = Money.FromDecimal(2, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var totalMoney = Money.FromDecimal(3, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act

            // Asset
            Assert.Equal(totalMoney, money1 + money2);
        }

        [Fact]
        public void Subtract_Of_Money_Should_Be_Equal_With_Total_Amount()
        {
            // Arrange
            var money1 = Money.FromDecimal(4, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var money2 = Money.FromDecimal(1, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);
            var totalMoney = Money.FromDecimal(3, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act

            // Asset
            Assert.Equal(totalMoney, money1 - money2);
        }
    }
}