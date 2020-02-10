using Catalog.WebClient.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.WebClient.HttpClients
{
    public class ProductHttpClient
    {
        private const string JsonMediaType = "application/json";
        private const string FormMediaType = "application/json";
        private readonly HttpClient _client;

        public ProductHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _client = httpClient;
            _client.BaseAddress = new Uri(configuration["ProductApiUri"]);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(string code = null, string name = null)
        {
            var requestUrl = $"/api/v1/products?code={code}&name={name}";
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
            var response = await _client.GetAsync(requestUrl);
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(await response.Content.ReadAsStringAsync());

            return products;
        }

        public async Task<HttpResponseMessage> CreateProductAsync(ProductViewModel product)
        {
            var formDataContent = new MultipartFormDataContent
            {
                { new StringContent(product.Code, Encoding.UTF8),  nameof(ProductViewModel.Code)},
                { new StringContent(product.Name, Encoding.UTF8), nameof(ProductViewModel.Name)},
                { new StringContent(product.Price.ToString(), Encoding.UTF8), nameof(ProductViewModel.Price) },
                { new StringContent("Euro", Encoding.UTF8), nameof(ProductViewModel.CurrencyCode) }
            };

            if (product.Photo != null && product.Photo.Length > 0)
            {
                var fileContent = new MemoryStream();
                await product.Photo.CopyToAsync(fileContent);
                fileContent.Position = 0;
                formDataContent.Add(new StreamContent(fileContent), "photo", product.Photo.FileName);
            }

            const string apiAddress = "/api/v1/products";
            var response = await _client.PostAsync(apiAddress, formDataContent);

            return response;
        }
    }
}