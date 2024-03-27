using AzureMicroservicesTest.Api.Search.Interfaces;
using AzureMicroservicesTest.Api.Search.Models;
using System.Text.Json;

namespace AzureMicroservicesTest.Api.Search.Services
{
    public class ProductService : IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;

        public ProductService(IHttpClientFactory _httpClientFactory, ILogger<OrderService> _logger)
        {
            httpClientFactory = _httpClientFactory;
            logger = _logger;
        }
        public async Task<(bool IsSuccess, Product? Product, string? Error)> GetProductAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductService");
                var response = await client.GetAsync($"api/products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Product>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError("Error:{ex}", ex);
                return (false, null, ex.Message);
            }
        }
    }
}
