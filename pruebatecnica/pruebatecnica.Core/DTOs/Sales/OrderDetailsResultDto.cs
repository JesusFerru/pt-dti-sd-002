namespace pruebatecnica.Domain.DTOs.Sales;
public class OrderDetailsResultDto
{
    public double Subtotal { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
}
