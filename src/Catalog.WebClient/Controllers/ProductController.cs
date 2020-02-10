using Catalog.WebClient.HttpClients;
using Catalog.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            var products = await _client.GetProductsAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            var response = await _client.CreateProductAsync(viewModel);
            if (response.StatusCode == HttpStatusCode.Created)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("Error", "Could not create product");

            return View();
        }
    }
}