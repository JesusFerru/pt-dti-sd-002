namespace pruebatecnica.Domain.DTOs;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string Method { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; } = decimal.Zero;
}
