using OrdersProcessingService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersProcessingService.Core.Application
{
    public interface IOrdersService
    {
        Task<Order> CreateOrder(Order order);
        Task<bool> DeleteOrder(Guid orderId);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetOrder(Guid orderId);
        Task<IEnumerable<OrdersByUser>> GetOrdersByUser();
        Task<bool> UpdateOrder(Order order);
    }
}