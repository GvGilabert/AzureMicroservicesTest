namespace AzureMicroservicesTest.Api.Orders.Db
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderItem>? Items { get; set; }
    }
}
