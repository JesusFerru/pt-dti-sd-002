namespace pruebatecnica.Domain.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public double Subtotal { get; set; } = 0.0;
    public double TaxAmount { get; set; } = 0.0;
    public double TotalAmount { get; set; } = 0.0;
    public string StatusOrder { get; set; } = "PagoPendiente";
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set;} = DateTime.Now;
}
