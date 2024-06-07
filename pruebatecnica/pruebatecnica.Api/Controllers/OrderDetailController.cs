using Microsoft.AspNetCore.Mvc;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDto>>> GetOrderDetails()
        {
            var orderDetails = await _orderDetailRepository.GetAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDto>> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailRepository.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailDto>> CreateOrderDetail(OrderDetailDto orderDetailDto)
        {
            try
            {
                var createdOrderDetail = await _orderDetailRepository.CreateAsync(orderDetailDto);
                return CreatedAtAction(nameof(GetOrderDetail), new { id = createdOrderDetail.OrderDetailId }, createdOrderDetail);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetailDto>> UpdateOrderDetail(int id, OrderDetailDto orderDetailDto)
        {
            if (id != orderDetailDto.OrderDetailId)
            {
                return BadRequest("OrderDetail ID mismatch");
            }

            var updatedOrderDetail = await _orderDetailRepository.UpdateAsync(id, orderDetailDto);
            if (updatedOrderDetail == null)
            {
                return NotFound($"OrderDetail with Id '{id}' not found");
            }

            return Ok(updatedOrderDetail);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetail(int id)
        {
            var existingOrderDetail = await _orderDetailRepository.GetById(id);
            if (existingOrderDetail == null)
            {
                return NotFound($"OrderDetail with Id '{id}' not found");
            }

            await _orderDetailRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
