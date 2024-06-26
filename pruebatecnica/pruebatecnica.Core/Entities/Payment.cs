﻿namespace pruebatecnica.Domain.Entities;

public class Payment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    [StringLength(100)]
    public string Method { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public double TotalAmount { get; set; } = 0.0;

    public Order Order { get; set; } = new Order();
    public Customer Customer { get; set; } = new Customer();
}
