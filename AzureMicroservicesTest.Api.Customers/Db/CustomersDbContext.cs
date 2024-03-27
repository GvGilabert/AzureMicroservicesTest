using Microsoft.EntityFrameworkCore;

namespace AzureMicroservicesTest.Api.Customers.Db
{
    public class CustomersDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
