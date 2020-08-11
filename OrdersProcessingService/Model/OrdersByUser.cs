using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersProcessingService.Api.Model
{
    public class OrdersByUser
    {
        public Guid UserId { get; set; }
        public Guid[] Orders { get; set; }
        public long TotalAmount { get; set; }
    }
}
