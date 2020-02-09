using Catalog.WebClient.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<IEnumerable<ProductViewModel>> GetProducts(string code = null, string name = null)
        {
            var requestUrl = $"/api/v1/products?code={code}&name={name}";
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
            var response = await _client.GetAsync(requestUrl);
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(await response.Content.ReadAsStringAsync());

            return products;
        }

        //public async ValueTask<bool> SendFileMessageAsync(List<int> targetLocationIds, string fileName, MemoryStream fileContent)
        //{
        //        var request = FileMessageToDictionary(new PVModel());

        //        var content = new MultipartFormDataContent();
        //        foreach (var (key, value) in request)
        //            content.Add(new StringContent(value, Encoding.UTF8), key);

        //        fileContent.Position = 0;
        //        content.Add(new StreamContent(fileContent), "file", fileName);

        //        const string apiAddress = "/api/v1/products";
        //        var response = await _client.PostAsync(apiAddress, content);

        //        return response.IsSuccessStatusCode;
        //}

        //private IEnumerable<KeyValuePair<string, string>> FileMessageToDictionary(object request)
        //{
        //    var res = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>(nameof(ProductViewModel.Code), request.Code ?? "" ),
        //        new KeyValuePair<string, string>(nameof(ProductViewModel.Name)), request.Name.ToString() ),
        //        new KeyValuePair<string, string>(nameof(ProductViewModel.Price)), request.Price ?? "" ),
        //    };

        //    return res;
        //}
    }
}