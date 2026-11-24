using Vaquita.Api.Models;

namespace Vaquita.Api.Services.Interfaces;

public interface IExpenseService {
    Task<IEnumerable<Expense>> GetAllAsync();

    Task<Expense?> GetByIdAsync(int id);

    Task AddAsync(Expense expense);

    Task UpdateAsync(Expense expense);

    Task DeleteAsync(int id);
}
