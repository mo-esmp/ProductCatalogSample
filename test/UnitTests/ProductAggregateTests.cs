using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Domain.Shared;
using System;
using Xunit;

namespace UnitTests
{
    public class ProductAggregateTests
    {
        [Fact]
        public void Create_Product_With_Null_Id_Should_Throw_Exception()
        {
            // Arrange
            var money = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Record.Exception(() => new Product(Guid.Empty, "12345", "Some name", money));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'Id')", exception.Message);
        }
    }
}