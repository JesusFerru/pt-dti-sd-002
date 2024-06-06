namespace pruebatecnica.Domain.Entities
{
    public class ShoppingCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }

        public Customer Customer { get; set; } = new Customer();
        public Product Product { get; set; } = new Product();
    }
}
