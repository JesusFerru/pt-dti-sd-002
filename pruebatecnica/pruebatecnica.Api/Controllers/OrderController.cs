using Microsoft.AspNetCore.Mvc;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pruebatecnica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderRepository.GetAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            try
            {
                var createdOrder = await _orderRepository.CreateAsync(orderDto);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.OrderId)
            {
                return BadRequest("Order ID mismatch");
            }

            var updatedOrder = await _orderRepository.UpdateAsync(id, orderDto);
            if (updatedOrder == null)
            {
                return NotFound($"Order with Id '{id}' not found");
            }

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _orderRepository.GetById(id);
            if (existingOrder == null)
            {
                return NotFound($"Order with Id '{id}' not found");
            }

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
