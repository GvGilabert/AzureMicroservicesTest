using AutoMapper;
using AzureMicroservicesTest.Api.Customers.Db;
using AzureMicroservicesTest.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AzureMicroservicesTest.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly ILogger<CustomerProvider> logger;
        private readonly CustomersDbContext customersDbContext;
        private readonly IMapper mapper;

        public CustomerProvider(ILogger<CustomerProvider> _logger, CustomersDbContext _customersDbContext, IMapper _mapper)
        {
            logger = _logger;
            customersDbContext = _customersDbContext;
            mapper = _mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!customersDbContext.Customers.Any())
            {
                customersDbContext.Customers.AddRange(
                [
                    new() {Id = 1, Name = "Juan", Address = "San Juan 2021" },
                    new() {Id = 2, Name = "Pedro", Address = "Patricios 1286" },
                    new() {Id = 3, Name = "Yan", Address = "Entre Rios 102" },
                    new() {Id = 4, Name = "Fabu", Address = "Martelli 500" },
                    new() {Id = 5, Name = "Alphonse", Address = "Randaso 50" },
                    new() {Id = 6, Name = "Martin", Address = "Pirulo 21" },
                ]);
                customersDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Customer? Customer, string? Error)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await customersDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer == null)
                {
                    return (false, null, "Not Found");
                }
                var result = mapper.Map<Models.Customer>(customer);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                logger.LogError("Some Error:{ex}",ex);
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer>? Customers, string? Error)> GetCustomersAsync()
        {
            try
            {
                var customers = await customersDbContext.Customers.ToListAsync();
                if (customers == null || customers.Count < 1)
                {
                    return (false, null, "Not Found");
                }
                var result = mapper.Map<IEnumerable<Models.Customer>>(customers);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                logger.LogError("Some Error:{ex}", ex);
                return (false, null, ex.ToString());
            }
        }
    }
}
