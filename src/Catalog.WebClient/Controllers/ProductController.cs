using Catalog.WebClient.HttpClients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalog.WebClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductHttpClient _client;

        public ProductController(ProductHttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _client.GetProducts();
            return View(products);
        }
    }
}