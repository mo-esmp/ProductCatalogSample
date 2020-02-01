using Product.Api.Domain.Shared;
using Xunit;

namespace UnitTests
{
    public class MoneyUnitTests
    {
        [Fact]
        public void Two_Of_Same_Currency_And_Amount_Should_Be_Equal()
        {
            var firstAmount = Money.FromDecimal(5, CurrencyCode.Euro, null);
            var secondAmount = Money.FromDecimal(5, CurrencyCode.Euro, null);

            Assert.Equal(firstAmount, secondAmount);
        }
    }
}