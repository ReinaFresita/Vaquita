using Vaquita.Api.Models;

namespace Vaquita.Api.Repositories.Interfaces;

public interface IExpenseRepository {
    Task<IEnumerable<Expense>> GetAllAsync();

    Task<Expense?> GetByIdAsync(int id);

    Task AddAsync(Expense expense);

    Task UpdateAsync(Expense expense);

    Task DeleteAsync(Expense expense); 
}
