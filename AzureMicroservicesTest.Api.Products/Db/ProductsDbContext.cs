using Microsoft.EntityFrameworkCore;

namespace AzureMicroservicesTest.Api.Products.Db
{
    public class ProductsDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductsDbContext(DbContextOptions options) : base (options)
        {
                
        }
        public ProductsDbContext()
        {
                
        }
    }
}
