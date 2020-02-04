using Catalog.Api;
using Catalog.Api.Domain.Shared;
using Catalog.Api.V1.Commands;
using IntegrationTests.TestSetup;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ProductControllerTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private const string _apiUrl = "/api/v1/products";
        private readonly HttpClient _client;

        public ProductControllerTests(TestingWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
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
            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(_apiUrl, content);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Create_Product_Returns_Ok()
        {
            // Arrange
            var command = new ProductAddCommand
            {
                Code = "AB12",
                Name = "Some Product",
                Price = 9.99m,
                CurrencyCode = CurrencyCode.Euro
            };
            var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(_apiUrl, content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}