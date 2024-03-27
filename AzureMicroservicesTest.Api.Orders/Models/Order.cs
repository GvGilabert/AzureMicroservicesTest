using AzureMicroservicesTest.Api.Orders.Db;

namespace AzureMicroservicesTest.Api.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get { return Items.Sum(x => x.UnitPrice * x.Qty); } }
        public IEnumerable<OrderItem> Items { get; set; } = [];
    }
}
