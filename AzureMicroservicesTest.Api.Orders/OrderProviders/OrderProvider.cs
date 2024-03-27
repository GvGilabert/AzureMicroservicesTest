using AutoMapper;
using AzureMicroservicesTest.Api.Orders.Db;
using AzureMicroservicesTest.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AzureMicroservicesTest.Api.Orders.OrderProviders
{
    public class OrderProvider : IOrdersProvider
    {
        private readonly ILogger<OrderProvider> logger;
        private readonly OrdersDbContext ordersDbContext;
        private readonly IMapper mapper;

        public OrderProvider(ILogger<OrderProvider> _logger, OrdersDbContext _ordersDbContext, IMapper _mapper)
        {
            logger = _logger;
            ordersDbContext = _ordersDbContext;
            mapper = _mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!ordersDbContext.Orders.Any())
            {
                ordersDbContext.Orders.AddRange(
                [
                    new Order() {Id = 1, CustomerId = 1, OrderDate = DateTime.Parse("05/05/2024"), 
                        Items =
                        [
                            new OrderItem() {Id = 1, OrderId = 1, ProductId = 1, Qty = 2, UnitPrice = 10},
                            new OrderItem() {Id = 2, OrderId = 1, ProductId = 2, Qty = 1, UnitPrice = 5},
                        ]},
                    new Order() {Id = 2, CustomerId = 3, OrderDate = DateTime.Parse("01/01/2022"), 
                        Items =
                        [
                            new OrderItem() {Id = 3, OrderId = 2, ProductId = 4, Qty = 7, UnitPrice = 45},
                            new OrderItem() {Id = 4, OrderId = 2, ProductId = 3, Qty = 5, UnitPrice = 23},
                        ]
                    }
                ]);
                ordersDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Order? Order, string? Error)> GetOrderAsync(int id)
        {
            try
            {
                var order = await ordersDbContext.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
                if (order == null)
                {
                    return (false, null, "Not Found");
                }
                var result = mapper.Map<Models.Order>(order);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                logger.LogError("Some Error:{ex}", ex);
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order>? Orders, string? Error)> GetOrdersAsync()
        {
            try
            {
                var orders = await ordersDbContext.Orders.Include(x => x.Items).ToListAsync();
                if (orders == null || orders.Count < 1)
                {
                    return (false, null, "Not Found");
                }
                var result = mapper.Map<IEnumerable<Models.Order>>(orders);
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
