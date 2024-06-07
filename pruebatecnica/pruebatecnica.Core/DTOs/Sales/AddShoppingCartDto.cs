namespace pruebatecnica.Domain.DTOs.Sales;

public class AddShoppingCartDto
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
