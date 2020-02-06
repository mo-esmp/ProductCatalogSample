using Catalog.Api.Domain.Exceptions;
using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Domain.Shared;
using Catalog.Api.Resources;
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

        [Fact]
        public void Create_Product_With_Null_Code_Should_Throw_Exception()
        {
            // Arrange
            var money = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Assert.Throws<NullArgumentDomainException>(() => new Product(Guid.NewGuid(), null, "Some name", money));

            // Assert
            Assert.Equal(
                string.Format(ErrorMessagesResource.NullArgumentException, DisplayNamesResource.ProductCode),
                exception.Message);
        }

        [Fact]
        public void Create_Product_With_With_Valid_Code_Should_Not_Throw_Exception()
        {
            // Arrange
            var money = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), "12345", "Some name", money));

            // Assert
            Assert.Null(exception);
        }
    }
}