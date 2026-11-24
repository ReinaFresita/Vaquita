using Microsoft.EntityFrameworkCore;
using Vaquita.Api.Data;
using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;

namespace Vaquita.Api.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly VaquitaContext _context;

    public PaymentRepository(VaquitaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetByGroupIdAsync(int groupId)
    {
        return await _context.Payments
            .Where(p => p.GroupId == groupId)
            .ToListAsync();
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }
}
