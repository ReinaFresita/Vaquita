using Vaquita.Api.Models;

namespace Vaquita.Api.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetByGroupIdAsync(int groupId);
    Task AddAsync(Payment payment);
}
