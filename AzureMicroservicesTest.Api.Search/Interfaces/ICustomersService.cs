using AzureMicroservicesTest.Api.Search.Models;

namespace AzureMicroservicesTest.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, Customer? Customer, string? Error)> GetCustomerAsync(int id);
    }
}
