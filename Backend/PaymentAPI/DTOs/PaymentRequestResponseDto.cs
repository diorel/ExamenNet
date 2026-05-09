namespace PaymentAPI.DTOs;

public class PaymentRequestResponseDto
{
    public int Id { get; set; }

    public string RequesterName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}
