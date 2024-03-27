using AzureMicroservicesTest.Api.Search.Interfaces;
using AzureMicroservicesTest.Api.Search.Models;
using System.Text.Json;

namespace AzureMicroservicesTest.Api.Search.Services
{
    public class OrderService : IOrdersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory _httpClientFactory, ILogger<OrderService> _logger)
        {
            httpClientFactory = _httpClientFactory;
            logger = _logger;
        }
        public async Task<(bool IsSuccess, Order? Order, string? Error)> GetOrderAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Order>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError("Error:{ex}", ex);
                return(false,null,ex.Message);
            }
        }
    }
}
