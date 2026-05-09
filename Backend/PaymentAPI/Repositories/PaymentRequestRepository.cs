using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using PaymentAPI.Entities;
using PaymentAPI.Repositories.Interfaces;

namespace PaymentAPI.Repositories;

public class PaymentRequestRepository : IPaymentRequestRepository
{
    private readonly AppDbContext _context;

    public PaymentRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PaymentRequest entity)
    {
        await _context.PaymentRequests.AddAsync(entity);
    }

    public async Task<IEnumerable<PaymentRequest>> GetAllAsync()
    {
        return await _context.PaymentRequests
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
