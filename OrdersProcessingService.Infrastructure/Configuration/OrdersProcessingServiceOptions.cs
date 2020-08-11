namespace OrdersProcessingService.Infrastructure
{
    public class OrdersProcessingServiceOptions
    {
        public string DatabaseConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
