using PaymentAPI.DTOs;
using PaymentAPI.Entities;
using PaymentAPI.Repositories.Interfaces;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.Services;

public class PaymentRequestService : IPaymentRequestService
{
    private readonly IPaymentRequestRepository _repository;

    private static readonly HashSet<string> AllowedCurrencies = new(StringComparer.OrdinalIgnoreCase)
    {
        "MXN", "USD"
    };

    public PaymentRequestService(IPaymentRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaymentRequestResponseDto> CreateAsync(CreatePaymentRequestDto dto)
    {
        if (!AllowedCurrencies.Contains(dto.Currency))
        {
            throw new ArgumentException($"Currency '{dto.Currency}' is not valid. Allowed values are: MXN, USD.");
        }

        var entity = new PaymentRequest
        {
            RequesterName = dto.RequesterName,
            Amount = dto.Amount,
            Currency = dto.Currency.ToUpper(),
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return MapToResponseDto(entity);
    }

    public async Task<IEnumerable<PaymentRequestResponseDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return entities.Select(MapToResponseDto);
    }

    private static PaymentRequestResponseDto MapToResponseDto(PaymentRequest entity)
    {
        return new PaymentRequestResponseDto
        {
            Id = entity.Id,
            RequesterName = entity.RequesterName,
            Amount = entity.Amount,
            Currency = entity.Currency,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt
        };
    }
}
