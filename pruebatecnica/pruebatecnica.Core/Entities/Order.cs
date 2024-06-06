namespace pruebatecnica.Domain.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Subtotal { get; set; } = decimal.Zero;
        public decimal TotalAmount { get; set; } = Decimal.Zero;
        public decimal TaxAmount { get; set; } = Decimal.Zero;
        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Customer Customer { get; set; } = new Customer();
    }
}
