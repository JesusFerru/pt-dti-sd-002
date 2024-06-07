using Microsoft.AspNetCore.Mvc;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace pruebatecnica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartDto>>> GetShoppingCarts()
        {
            var shoppingCarts = await _shoppingCartRepository.GetAsync();
            return Ok(shoppingCarts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(int id)
        {
            var shoppingCart = await _shoppingCartRepository.GetById(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            return Ok(shoppingCart);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> CreateShoppingCart(ShoppingCartDto shoppingCartDto)
        {
            try
            {
                var createdShoppingCart = await _shoppingCartRepository.CreateAsync(shoppingCartDto);
                return CreatedAtAction(nameof(GetShoppingCart), new { id = createdShoppingCart.ShoppingCartId }, createdShoppingCart);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> UpdateShoppingCart(int id, ShoppingCartDto shoppingCartDto)
        {
            if (id != shoppingCartDto.ShoppingCartId)
            {
                return BadRequest("ShoppingCart ID mismatch");
            }

            var updatedShoppingCart = await _shoppingCartRepository.UpdateAsync(id, shoppingCartDto);
            if (updatedShoppingCart == null)
            {
                return NotFound($"ShoppingCart with Id '{id}' not found");
            }

            return Ok(updatedShoppingCart);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShoppingCart(int id)
        {
            var existingShoppingCart = await _shoppingCartRepository.GetById(id);
            if (existingShoppingCart == null)
            {
                return NotFound($"ShoppingCart with Id '{id}' not found");
            }

            await _shoppingCartRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
