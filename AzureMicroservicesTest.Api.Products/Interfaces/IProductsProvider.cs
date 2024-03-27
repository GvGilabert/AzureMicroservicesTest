using AzureMicroservicesTest.Api.Products.Models;

namespace AzureMicroservicesTest.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMsg)> GetProductsAsync();
        Task<(bool IsSuccess, Product? Products, string? ErrorMsg)> GetProductAsync(int id);

    }
}
