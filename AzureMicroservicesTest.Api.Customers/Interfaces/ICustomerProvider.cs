using AzureMicroservicesTest.Api.Customers.Models;

namespace AzureMicroservicesTest.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer>? Customers, string? Error)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer? Customer, string? Error)> GetCustomerAsync(int id);
    }
}
