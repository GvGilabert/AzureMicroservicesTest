using Microsoft.EntityFrameworkCore;

namespace AzureMicroservicesTest.Api.Orders.Db
{
    public class OrdersDbContext (DbContextOptions options) : DbContext(options)
    { 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
