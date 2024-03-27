using AzureMicroservicesTest.Api.Search.Models;

namespace AzureMicroservicesTest.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, Product? Product, string? Error)> GetProductAsync(int id);
    }
}
