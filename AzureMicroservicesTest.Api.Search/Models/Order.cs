namespace AzureMicroservicesTest.Api.Search.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal Total { get { return Items.Sum(x => x.UnitPrice * x.Qty); } }
        public IEnumerable<OrderItem> Items { get; set; } = [];
    }
}
