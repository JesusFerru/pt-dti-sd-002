namespace pruebatecnica.Domain.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal Subtotal { get; set; } = decimal.Zero;
    public decimal TotalAmount { get; set; } = decimal.Zero;
    public decimal TaxAmount { get; set; } = decimal.Zero;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
