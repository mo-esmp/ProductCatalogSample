using Catalog.Api;
using Catalog.Api.Domain.Shared;
using Catalog.Api.V1.Commands;
using IntegrationTests.TestSetup;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IntegrationTests
{
    public class ProductControllerTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private const string ApiUrl = "/api/v1/products";
        private readonly HttpClient _client;

        public ProductControllerTests(TestingWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task Create_Duplicate_Product_Returns_Error()
        {
            // Arrange
            var command = new ProductAddCommand
            {
                Code = InitialProductList.Products.First().Code.Value,
                Name = "Some Product",
                Price = 9.99m,
                CurrencyCode = CurrencyCode.Euro
            };
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(ApiUrl, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task Create_Product_Returns_Ok()
        {
            // Arrange
            var formDataContent = new MultipartFormDataContent
            {
                { new StringContent("AB12", Encoding.UTF8),  nameof(ProductAddCommand.Code)},
                { new StringContent("Some Product", Encoding.UTF8), nameof(ProductAddCommand.Name)},
                { new StringContent("9.99", Encoding.UTF8), nameof(ProductAddCommand.Price) },
                { new StringContent("Euro", Encoding.UTF8), nameof(ProductAddCommand.CurrencyCode) }
            };

            // Act
            var response = await _client.PostAsync(ApiUrl, formDataContent);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task Update_Product_With_Wrong_Id_Returns_Error()
        {
            // Arrange
            var command = new ProductEditCommand
            {
                Code = InitialProductList.Products.First().Code.Value,
                Name = "Some Product",
                Price = 9.99m,
                CurrencyCode = CurrencyCode.Euro
            };
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"{ApiUrl}/{Guid.NewGuid()}", content);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task Update_Product_With_Duplicate_Code_Returns_Error()
        {
            // Arrange
            var command = new ProductEditCommand
            {
                Code = InitialProductList.Products.ElementAt(1).Code.Value,
                Name = "Some Product",
                Price = 9.99m,
                CurrencyCode = CurrencyCode.Euro
            };
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"{ApiUrl}/{InitialProductList.Products.First().Id}", content);
            var body = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task Update_Product_Returns_Ok()
        {
            // Arrange
            var product = InitialProductList.Products.First();
            var command = new ProductEditCommand
            {
                Code = product.Code.Value,
                Name = "Some Product",
                Price = 9.99m,
                CurrencyCode = CurrencyCode.Euro
            };
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"{ApiUrl}/{product.Id}", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(1)]
        public async Task Delete_Product_With_Wrong_Id_Returns_Error()
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync($"{ApiUrl}/{Guid.NewGuid()}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task Delete_Product_Returns_Ok()
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync($"{ApiUrl}/{InitialProductList.Products.Last().Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}