namespace pruebatecnica.Domain.Entities;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;
    public int PhoneNumber { get; set; } = int.MaxValue;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}
