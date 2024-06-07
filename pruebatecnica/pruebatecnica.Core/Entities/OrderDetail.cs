namespace pruebatecnica.Domain.Entities;

public class OrderDetail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Order Order { get; set; } = new Order();
    public Product Product { get; set; } = new Product();
}
