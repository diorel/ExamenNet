using PaymentAPI.DTOs;

namespace PaymentAPI.Services.Interfaces;

public interface IPaymentRequestService
{
    Task<PaymentRequestResponseDto> CreateAsync(CreatePaymentRequestDto dto);
    Task<IEnumerable<PaymentRequestResponseDto>> GetAllAsync();
}
