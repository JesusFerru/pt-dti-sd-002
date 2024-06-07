namespace pruebatecnica.Domain.DTOs;

public class ShoppingCartDto
{
    public int ShoppingCartId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.Now;
    public bool isOrdered { get; set; }
}
