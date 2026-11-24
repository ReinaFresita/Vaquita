using Vaquita.Api.Models;

namespace Vaquita.Api.Services.Interfaces;

public interface IPaymentService
{
    Task<IEnumerable<Payment>> GetByGroupIdAsync(int groupId);
    Task AddAsync(int groupId, Payment payment);
}
