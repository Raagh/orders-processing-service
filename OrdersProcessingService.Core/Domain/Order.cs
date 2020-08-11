using System;

namespace OrdersProcessingService.Core.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
    }
}
