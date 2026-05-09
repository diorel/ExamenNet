using PaymentAPI.Entities;

namespace PaymentAPI.Repositories.Interfaces;

public interface IPaymentRequestRepository
{
    Task AddAsync(PaymentRequest entity);
    Task<IEnumerable<PaymentRequest>> GetAllAsync();
    Task SaveChangesAsync();
}
