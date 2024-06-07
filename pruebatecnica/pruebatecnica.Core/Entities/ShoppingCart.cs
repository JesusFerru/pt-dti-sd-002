namespace pruebatecnica.Domain.Entities;

public class ShoppingCart
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShoppingCartId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 0;
    public DateTime AddedAt { get; set; } = DateTime.Now;
    public bool isOrdered { get; set; } = false;
    public Customer Customer { get; set; } = new Customer();
    public Product Product { get; set; } = new Product();
}
