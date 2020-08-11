using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using OrdersProcessingService.Core.Application;
using OrdersProcessingService.Core.Domain;
using System.Linq.Expressions;

namespace OrdersProcessingService.Infrastructure.DataAccess
{
    public class OrdersRespository : IOrdersRespository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Order> collection;
        private readonly ILogger<OrdersRespository> logger;

        public OrdersRespository(IOptions<OrdersProcessingServiceOptions> options, ILogger<OrdersRespository> logger)
        {
            var configuration = options.Value;
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var client = new MongoClient(configuration.DatabaseConnectionString);

            this.database = client.GetDatabase(configuration.DatabaseName);
            this.collection = database.GetCollection<Order>(configuration.CollectionName);
            this.logger = logger;
        }

        public async Task<Order> GetOrder(Guid orderId) => await collection.Find(x => x.Id == orderId).FirstOrDefaultAsync();

        public async Task<IEnumerable<Order>> GetAll() => await collection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<OrdersByUser>> GetOrdersByUser() =>
            await this.collection
                .Aggregate()
                .Group(x => x.UserId, y => new OrdersByUser
                {
                    UserId = y.Key,
                    Orders = y.Select(x => x.Id).ToArray(),
                    TotalAmount = y.Sum(x => x.Amount)
                }).ToListAsync();

        public async Task<Order> CreateOrder(Order order)
        {
            try
            {
                await collection.InsertOneAsync(order);

                return order;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            try
            {
                var result = await collection.DeleteOneAsync(x => x.Id == orderId);

                return result.IsAcknowledged && result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                var result = await collection.ReplaceOneAsync(x => x.Id == order.Id, order);

                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}