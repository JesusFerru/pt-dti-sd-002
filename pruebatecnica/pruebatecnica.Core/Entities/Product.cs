namespace pruebatecnica.Domain.Entities;

public class Product
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;
    [Required]
    public double UnitPrice { get; set; } = 0.0;
    [Required]
    public int Stock { get; set; } = 0;
    [StringLength(250)]
    public string Description { get; set; } = string.Empty;

}
