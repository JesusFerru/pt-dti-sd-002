using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.DTOs.Sales;
using pruebatecnica.Domain.Entities;
using pruebatecnica.Domain.Wrapper;

namespace pruebatecnica.Application.Interfaces.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    public async Task<ApiResponse<IEnumerable<ShoppingCartDto>>> GetCustomerCartAvailable(int customerId)
    {
        try
        {
            // Obtener los elementos del carrito del cliente que no han sido ordenados
            var cartItems = await _shoppingCartRepository.GetByCustomerId(customerId);
            var availableItems = new List<ShoppingCartDto>();

            foreach (var item in cartItems)
            {
                if (!item.isOrdered)
                {
                    availableItems.Add(item);
                }
            }

            var message = availableItems.Count > 0 ? "Lista obtenida exitosamente" : "No hay ningún producto agregado al carrito";
            return new ApiResponse<IEnumerable<ShoppingCartDto>>(availableItems, message);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<ShoppingCartDto>>($"Error in GetCustomerCartAvailable: {ex.Message}");
        }
    }

    public async Task<ApiResponse<ShoppingCartDto>> AddItemsToCart(AddShoppingCartDto item)
    {
        try
        {
            var product = await _productRepository.GetById(item.ProductId);

            if (product.Stock >= item.Quantity)
            {
                // Agregar el producto al carrito
                ShoppingCartDto shoppingCart = new ShoppingCartDto
                {
                    CustomerId = item.CustomerId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    AddedAt = DateTime.Now,
                    isOrdered = false
                };

                var data = await _shoppingCartRepository.CreateAsync(shoppingCart);

                return new ApiResponse<ShoppingCartDto>(data, $"Producto {item.ProductId} agregado al carrito correctamente");
            }
            else
            {
                return new ApiResponse<ShoppingCartDto>("No hay suficiente stock disponible para el producto seleccionado");
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<ShoppingCartDto>($"Error in AddToCartAsync: {ex.Message}");
        }
    }

    public async Task<ApiResponse<ShoppingCartDto>> UpdateCartItemQuantity(AddShoppingCartDto updateItem)
    {
        try
        {
            var cartItems = await _shoppingCartRepository.GetByCustomerId(updateItem.CustomerId);
            var item = cartItems.FirstOrDefault(item => item.ProductId == updateItem.ProductId && !item.isOrdered);

            if (item == null)
            {
                return new ApiResponse<ShoppingCartDto>("Producto no encontrado en el carrito");
            }

            var product = await _productRepository.GetById(updateItem.ProductId);

            if (product.Stock < updateItem.Quantity)
            {
                return new ApiResponse<ShoppingCartDto>("No hay suficiente stock disponible para actualizar la cantidad");
            }

            item.Quantity = updateItem.Quantity;

            var data = await _shoppingCartRepository.UpdateAsync(item.ShoppingCartId, item);

            return new ApiResponse<ShoppingCartDto>(data, $"El producto {updateItem.ProductId} fue actualizada a {updateItem.Quantity} items");
        }
        catch (Exception ex)
        {
            return new ApiResponse<ShoppingCartDto>($"Error in UpdateCartItemQuantity: {ex.Message}");
        }
    }

    public async Task<ApiResponse<ShoppingCartDto>> UpdateCartItemStatus(int shoppingCartId)
    {
        try
        {
            var cartItem = await _shoppingCartRepository.GetById(shoppingCartId);
            if (cartItem == null)
            {
                return new ApiResponse<ShoppingCartDto>("El elemento del carrito no fue encontrado.");
            }

            cartItem.isOrdered = true;
            await _shoppingCartRepository.UpdateAsync(shoppingCartId, cartItem);

            return new ApiResponse<ShoppingCartDto>(cartItem, "Estado del elemento del carrito actualizado correctamente.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<ShoppingCartDto>($"Error in UpdateCartItemStatus: {ex.Message}");
        }
    }

    public async Task<ApiResponse<bool>> RemoveItemFromCart(int cartItemId)
    {
        try
        {
            await _shoppingCartRepository.DeleteAsync(cartItemId);
            return new ApiResponse<bool>(true, $"Item {cartItemId} removido del carrito correctamente");
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>($"Error in RemoveItemFromCart: {ex.Message}");
        }
    }

    public async Task<ApiResponse<bool>> RemoveAllItemsNotOrderedFromCart(int customerId)
    {
        try
        {
            var cartItems = await _shoppingCartRepository.GetByCustomerId(customerId);

            var itemsToRemove = cartItems.Where(item => !item.isOrdered).ToList();

            if (itemsToRemove.Any())
            {
                foreach (var item in itemsToRemove)
                {
                    await _shoppingCartRepository.DeleteAsync(item.ShoppingCartId);
                }
                return new ApiResponse<bool>(true, "Todos los artículos disponibles en el carrito han sido removidos");
            }
            else
            {
                return new ApiResponse<bool>(true, "No hay artículos en el carrito para remover");
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>($"Error in RemoveAllItemsNotOrderedFromCart: {ex.Message}");
        }
    }
}
