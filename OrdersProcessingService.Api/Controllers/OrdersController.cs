using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrdersProcessingService.Api.Model;
using OrdersProcessingService.Core.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AutoMapper;

namespace OrdersProcessingService.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves all orders
        /// </summary>
        /// <returns>a list with all the orders available</returns>
        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll()
        {
            var result = await ordersService.GetAll();

            return mapper.Map<IEnumerable<Core.Domain.Order>, IEnumerable<Order>>(result);
        }

        /// <summary>
        /// Retrieves the order that matches the id supplied
        /// </summary>
        /// <returns>one order model</returns>
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> Get(Guid orderId)
        {
            var result = await ordersService.GetOrder(orderId);

            if (result == null) return NotFound();

            var response = mapper.Map<Core.Domain.Order, Order>(result);

            return Ok(response);
        }

        /// <summary>
        /// Deletes the specified order
        /// </summary>
        [HttpDelete("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            var result = await ordersService.DeleteOrder(orderId);

            if (!result) return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Updates the specified order
        /// </summary>
        /// <returns>the updated order</returns>
        [HttpPut("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid orderId, [FromBody]OrderCreateUpdate orderUpdate)
        {
            var order = new Core.Domain.Order()
            {
                Id = orderId,
                Amount = orderUpdate.Amount,
                UserId = orderUpdate.UserId
            };

            var result = await ordersService.UpdateOrder(order);

            if (!result) return BadRequest();

            return Ok();
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <returns>the newly created order</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> Post([FromBody]OrderCreateUpdate orderRequest)
        {
            var order = new Core.Domain.Order()
            {
                Amount = orderRequest.Amount,
                UserId = orderRequest.UserId
            };

            var result = await ordersService.CreateOrder(order);

            if (result == null) return BadRequest();

            var response = mapper.Map<Core.Domain.Order, Order>(result);

            return StatusCode(201, response);
        }

        /// <summary>
        /// Retrieves all orders grouped by user with the amount spend by each user
        /// </summary>
        /// <returns>a list of orders by user models</returns>
        [HttpGet("by-user")]
        public async Task<IEnumerable<OrdersByUser>> GetOrdersByUser()
        {
            var result = await ordersService.GetOrdersByUser();

            return mapper.Map<IEnumerable<Core.Domain.OrdersByUser>, IEnumerable<OrdersByUser>>(result);
        }
    }
}
