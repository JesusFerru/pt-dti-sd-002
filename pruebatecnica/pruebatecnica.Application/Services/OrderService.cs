using pruebatecnica.Application.Interfaces;
using pruebatecnica.Application.Interfaces.Services;
using pruebatecnica.Domain.DTOs;
using pruebatecnica.Domain.DTOs.Sales;
using pruebatecnica.Domain.Wrapper;

namespace pruebatecnica.Application.Services;
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IProductRepository _productRepository;
    private readonly IShoppingCartService _shoppingCartService;

    public OrderService(
        IOrderRepository orderRepository, 
        IOrderDetailRepository orderDetailRepository, 
        IProductRepository productRepository,
        IShoppingCartService shoppingCartService)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _productRepository = productRepository;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<ApiResponse<OrderResultDto>> GenerateOrder(int customerId)
    {
        // Obtener los elementos del carrito del cliente que no han sido ordenados
         var cartItems = await _shoppingCartService.GetCustomerCartAvailable(customerId);

        if (cartItems.Data == null || !cartItems.Data.Any())
        {
            return new ApiResponse<OrderResultDto>("No hay elementos en el carrito para generar una orden.");
        }

        // Crear la orden
        var order = await CreateOrder(customerId);

        // Agregar los elementos del carrito a los detalles de la orden y calcular los totales
        var orderDetailsList = await AddOrderDetails(order.OrderId, cartItems.Data.ToList());

        // Actualizar la orden con los totales calculados
        var newOrder = await UpdateOrderTotals(order, orderDetailsList.Subtotal);
        var details = orderDetailsList.OrderDetails;

        var data = new OrderResultDto(newOrder, details);
      

        return new ApiResponse<OrderResultDto>(data, "Orden generada exitosamente.");
    }


    protected async Task<OrderDto> CreateOrder(int customerId)
    {
        var order = new OrderDto
        {
            CustomerId = customerId,
            StatusOrder = "PagoPendiente",
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await _orderRepository.CreateAsync(order);
        return order;
    }

    protected async Task<OrderDetailsResultDto> AddOrderDetails(int OrderId, List<ShoppingCartDto> cartItemsList)
    {
        //Este metodo añade cada uno de los productos agregados al carrito hacia OrderDetail
        //Además va almacenando la suma de cada uno de los productos en subtotal
        /*Por último actualiza el valor IsOrdered de los productos del carrito del cliente para cambiarlos a true
          y asi dejan de ser visibles en el carrito del Cliente */
        double subtotal = 0;
        var orderDetails = new List<OrderDetailDto>();

        foreach (var cartItem in cartItemsList)
        {
            var product = await _productRepository.GetById(cartItem.ProductId);
            double itemTotal = product.UnitPrice * cartItem.Quantity;
            subtotal += itemTotal;

            var orderDetail = new OrderDetailDto
            {
                OrderId = OrderId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = itemTotal
            };
            await _orderDetailRepository.CreateAsync(orderDetail);
            orderDetails.Add(orderDetail);

            await _shoppingCartService.UpdateCartItemStatus(cartItem.ShoppingCartId);
        }

        return new OrderDetailsResultDto
        {
            Subtotal = subtotal,
            OrderDetails = orderDetails
        };
    }

    protected async Task<OrderDto> UpdateOrderTotals(OrderDto order, double subtotal)
    {
        double taxAmount = subtotal * 0.13;
        double totalAmount = subtotal + taxAmount;

        order.Subtotal = subtotal;
        order.TaxAmount = taxAmount;
        order.TotalAmount = totalAmount;
        order.UpdatedAt = DateTime.Now;
        var data = await _orderRepository.UpdateAsync(order.OrderId, order);
        return data;
    }

    public Task<ApiResponse<bool>> CreatePayment(PaymentDto paymentDto)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<IEnumerable<OrderDto>>> GetCustomerOrders(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<IEnumerable<OrderDto>>> GetOrderByStatus(string statusOrder)
    {
        throw new NotImplementedException();
    }
}
