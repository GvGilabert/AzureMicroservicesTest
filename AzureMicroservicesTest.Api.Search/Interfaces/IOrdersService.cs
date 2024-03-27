using AzureMicroservicesTest.Api.Search.Models;

namespace AzureMicroservicesTest.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, Order? Order, string? Error)> GetOrderAsync(int id);
    }
}
