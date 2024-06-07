using Microsoft.AspNetCore.Mvc;
using pruebatecnica.Application.Interfaces.Services;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.DTOs.Sales;
using pruebatecnica.Domain.Wrapper;

namespace pruebatecnica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IOrderService _orderService;

    public SalesController(IShoppingCartService shoppingCartService, IOrderService orderService)
    {
        _shoppingCartService = shoppingCartService;
        _orderService = orderService;
    }

    [HttpGet("customer-cart/{customerId:int}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ShoppingCartDto>>>> GetCustomerCartAvailable(int customerId)
    {
        var response = await _shoppingCartService.GetCustomerCartAvailable(customerId);
        if (response.Succeeded)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost("add-to-cart")]
    public async Task<ActionResult<ApiResponse<string>>> AddToCartAsync(AddShoppingCartDto item)
    {
        var response = await _shoppingCartService.AddItemsToCart(item);
        if (response.Succeeded)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPatch("update-cart-item-quantity")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateCartItemQuantity([FromBody] AddShoppingCartDto updateItem)
    {
        var response = await _shoppingCartService.UpdateCartItemQuantity(updateItem);
        if (response.Succeeded)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpDelete("remove-from-cart/{cartItemId}")]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveFromCartAsync(int cartItemId)
    {
        // Implementación pendiente
        var response = await _shoppingCartService.RemoveItemFromCart(cartItemId);
        if (response.Succeeded)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpDelete("remove-all-items-not-ordered/{customerId}")]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveAllItemsNotOrderedFromCart(int customerId)
    {
        var response = await _shoppingCartService.RemoveAllItemsNotOrderedFromCart(customerId);
        if (response.Succeeded)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPost("generate-order/{customerId}")]
    public async Task<IActionResult> GenerateOrder(int customerId)
    {
        try
        {
            var response = await _orderService.GenerateOrder(customerId);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<OrderResultDto>($"Ocurrió un error al generar la orden: {ex.Message}"));
        }
    }

}


 
