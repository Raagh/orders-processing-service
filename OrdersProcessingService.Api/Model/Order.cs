using System;

namespace OrdersProcessingService.Api.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
    }
}
