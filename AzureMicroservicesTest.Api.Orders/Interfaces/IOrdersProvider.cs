using AzureMicroservicesTest.Api.Orders.Models;

namespace AzureMicroservicesTest.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order>? Orders, string? Error)> GetOrdersAsync();
        Task<(bool IsSuccess, Order? Order, string? Error)> GetOrderAsync(int id);
    }
}
