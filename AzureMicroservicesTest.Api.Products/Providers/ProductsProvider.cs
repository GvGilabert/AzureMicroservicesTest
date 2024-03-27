using AutoMapper;
using AzureMicroservicesTest.Api.Products.Db;
using AzureMicroservicesTest.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AzureMicroservicesTest.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext productsDbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext _productsDbContext, ILogger<ProductsProvider> _logger, IMapper _mapper)
        {
            productsDbContext = _productsDbContext;
            logger = _logger;
            mapper = _mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!productsDbContext.Products.Any()) 
            {
                productsDbContext.Products.AddRange(new Product[]
                {
                    new(){ Id = 1, Name ="Sticker", Inventory = 3, Price = 3.50m },
                    new(){ Id = 2, Name ="Keyboard", Inventory = 2, Price = 100.00m },
                    new(){ Id = 3, Name ="Processor", Inventory = 5, Price = 250m },
                    new(){ Id = 4, Name ="Mouse", Inventory = 1, Price = 50m },
                    new(){ Id = 5, Name ="Monitor", Inventory = 5, Price = 150m },
                    new(){ Id = 6, Name ="Keycap", Inventory = 2, Price = 2.49m },
                });
                productsDbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product>? Products, string? ErrorMsg)> GetProductsAsync()
        {
            try
            {
                var products = await productsDbContext.Products.ToListAsync();
                if (products != null && products.Count > 0)
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError("Some error:{ex}",ex);
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, Models.Product? Products, string? ErrorMsg)> GetProductAsync(int id)
        {
            try
            {
                var product = await productsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError("Some error:{ex}", ex);
                return (false, null, ex.ToString());
            }
        }
    }
}
