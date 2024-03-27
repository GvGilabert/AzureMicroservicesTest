using AzureMicroservicesTest.Api.Search.Interfaces;
using System.Runtime.InteropServices;

namespace AzureMicroservicesTest.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService _ordersService, IProductsService _productsService, ICustomersService _customerService)
        {
            ordersService = _ordersService;
            productsService = _productsService;
            customersService = _customerService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResult)> SearchAsync(int orderId)
        {
            var order = await ordersService.GetOrderAsync(orderId);
            if (order.IsSuccess)
            {
                var customer = await customersService.GetCustomerAsync(order.Order!.CustomerId);
                order.Order.CustomerName = customer.Customer?.Name ?? "Customer information is not available";
                foreach(var item in order.Order.Items)
                {
                    //TODO: Get all in one call
                    var product = await productsService.GetProductAsync(item.ProductId);
                    item.Name = product.Product?.Name ?? "Product information is not available";
                }
                return (true, order.Order);
            }
            return(false, null);
        }
    }
}
