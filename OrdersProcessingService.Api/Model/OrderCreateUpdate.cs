using System;

namespace OrdersProcessingService.Api.Model
{
    public class OrderCreateUpdate
    {
        public Guid UserId { get; set; }
        public int Amount { get; set; }
    }
}
