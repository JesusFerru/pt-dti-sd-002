namespace pruebatecnica.Domain.DTOs
{
    public class CreateCustomerDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int PhoneNumber { get; set; } = int.MaxValue;

    }
}
