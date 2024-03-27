using AzureMicroservicesTest.Api.Search.Interfaces;
using AzureMicroservicesTest.Api.Search.Models;
using System.Text.Json;

namespace AzureMicroservicesTest.Api.Search.Services
{
    public class CustomerService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory _httpClientFactory, ILogger<CustomerService> _logger)
        {
            httpClientFactory = _httpClientFactory;
            logger = _logger;
        }
        public async Task<(bool IsSuccess, Customer? Customer, string? Error)> GetCustomerAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomerService");
                var response = await client.GetAsync($"api/customers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Customer>(content, options);

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