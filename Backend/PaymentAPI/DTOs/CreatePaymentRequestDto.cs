using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.DTOs;

public class CreatePaymentRequestDto
{
    [Required(ErrorMessage = "RequesterName is required.")]
    [MaxLength(100, ErrorMessage = "RequesterName cannot exceed 100 characters.")]
    public string RequesterName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Currency is required.")]
    [MaxLength(3, ErrorMessage = "Currency cannot exceed 3 characters.")]
    public string Currency { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string? Description { get; set; }
}
