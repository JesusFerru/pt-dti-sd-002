namespace pruebatecnica.Domain.Entities;

public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public double Subtotal { get; set; } = 0.0;
    public double TaxAmount { get; set; } = 0.0;
    public double TotalAmount { get; set; } = 0.0;
    public string StatusOrder { get; set; } = "PagoPendiente";
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public Customer Customer { get; set; } = new Customer();
}
