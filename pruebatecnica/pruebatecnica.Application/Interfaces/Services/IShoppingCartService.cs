using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.DTOs.Sales;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Domain.Wrapper;

namespace pruebatecnica.Application.Interfaces.Services;

public interface IShoppingCartService
{

    Task<ApiResponse<IEnumerable<ShoppingCartDto>>> GetCustomerCartAvailable(int customerId);
    Task<ApiResponse<ShoppingCartDto>> AddItemsToCart(AddShoppingCartDto item);
    Task<ApiResponse<ShoppingCartDto>> UpdateCartItemQuantity(AddShoppingCartDto updateItem);
    Task<ApiResponse<ShoppingCartDto>> UpdateCartItemStatus(int shoppingCartId);
    Task<ApiResponse<bool>> RemoveItemFromCart(int cartItemId);
    Task<ApiResponse<bool>> RemoveAllItemsNotOrderedFromCart(int customerId);
}
