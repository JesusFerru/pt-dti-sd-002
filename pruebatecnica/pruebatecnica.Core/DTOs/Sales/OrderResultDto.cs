namespace pruebatecnica.Domain.DTOs.Sales;

public class OrderResultDto
{ 
    OrderDto NewOrder { get; set; } = new OrderDto();
    List<OrderDetailDto> Details { get; set; } = new List<OrderDetailDto>();

    public OrderResultDto( OrderDto orderDto, List<OrderDetailDto> details )
    {
        NewOrder = orderDto;
        Details = details;
    }

}
