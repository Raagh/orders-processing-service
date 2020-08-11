using OrdersProcessingService.Core.Application;
using OrdersProcessingService.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersProcessingService.Core.Application
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRespository ordersRespository;

        public OrdersService(IOrdersRespository ordersRespository)
        {
            this.ordersRespository = ordersRespository;
        }

        public async Task<IEnumerable<OrdersByUser>> GetOrdersByUser() => await ordersRespository.GetOrdersByUser();

        public async Task<IEnumerable<Order>> GetAll() => await ordersRespository.GetAll();

        public async Task<Order> GetOrder(Guid orderId)
        {
            if (orderId == Guid.Empty) return null;

            return await ordersRespository.GetOrder(orderId);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            if (order.UserId == Guid.Empty || order.Amount < 0) return null;

            return await ordersRespository.CreateOrder(order);
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            if (orderId == Guid.Empty) return false;

            return await ordersRespository.DeleteOrder(orderId);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            if (order.Id == Guid.Empty || order.UserId == Guid.Empty || order.Amount < 0) return false;

            return await ordersRespository.UpdateOrder(order);
        }
    }
}
