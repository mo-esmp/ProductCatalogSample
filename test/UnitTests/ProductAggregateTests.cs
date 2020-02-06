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
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), null, "Some name", money));

            // Assert
            Assert.IsType<NullArgumentDomainException>(exception);
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
            exception ??= new Exception("dummy");

            // Assert
            Assert.NotEqual(
                string.Format(ErrorMessagesResource.NullArgumentException, DisplayNamesResource.ProductCode),
                exception.Message);
        }

        [Fact]
        public void Create_Product_With_Null_Name_Should_Throw_Exception()
        {
            // Arrange
            var money = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), "12345", null, money));

            // Assert
            Assert.IsType<NullArgumentDomainException>(exception);
            Assert.Equal(
                string.Format(ErrorMessagesResource.NullArgumentException, DisplayNamesResource.ProductName),
                exception.Message);
        }

        [Fact]
        public void Create_Product_With_With_Valid_Name_Should_Not_Throw_Exception()
        {
            // Arrange
            var money = Money.FromDecimal(5, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), "12345", "Product Name", money));
            exception ??= new Exception("dummy");

            // Assert
            Assert.NotEqual(
                string.Format(ErrorMessagesResource.NullArgumentException, DisplayNamesResource.ProductName),
                exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Create_Product_With_Price_Zero_Should_Throw_Exception(int amount)
        {
            // Arrange
            var money = Money.FromDecimal(amount, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), "12345", "Product name", money));

            // Assert
            Assert.IsType<InvalidPriceDomainException>(exception);
            Assert.Equal(string.Format(ErrorMessagesResource.InvalidProductPriceError), exception.Message);
        }

        [Fact]
        public void Create_Product_With_Price_Is_Higher_Than_Zero_Should_Not_Throw_Exception()
        {
            // Arrange
            var money = Money.FromDecimal(1, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), "12345", "Product name", money));
            exception ??= new Exception("dummy");

            // Assert
            Assert.NotEqual(string.Format(ErrorMessagesResource.InvalidProductPriceError), exception.Message);
        }

        [Theory]
        [InlineData(10.99)]
        [InlineData(999)]
        public void Create_Product_With_Price_Lower_Than_999_Should_Be_Active(decimal amount)
        {
            // Arrange
            var money = Money.FromDecimal(amount, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var product = new Product(Guid.NewGuid(), "12345", "Product name", money);

            // Assert
            Assert.Equal(ProductStatus.Active, product.Status);
        }

        [Theory]
        [InlineData(999.99)]
        public void Create_Product_With_Price_Greater_Than_999_Should_Be_Pending(decimal amount)
        {
            // Arrange
            var money = Money.FromDecimal(amount, CurrencyCode.Euro, FakeCurrencyLookup.Lookup);

            // Act
            var product = new Product(Guid.NewGuid(), "12345", "Product name", money);

            // Assert
            Assert.Equal(ProductStatus.Pending, product.Status);
        }
    }
}