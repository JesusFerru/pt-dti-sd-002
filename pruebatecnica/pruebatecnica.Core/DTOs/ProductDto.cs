namespace pruebatecnica.Domain.DTOs;

public class ProductDto
{
    public string Category { get; set; } = string.Empty;
    public double UnitPrice { get; set; } = 0.0;
    public int Stock { get; set; } = 0;
    public string Description { get; set; } = string.Empty;

}
