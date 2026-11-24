using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Services;

public class ExpenseService : IExpenseService 
{
    private readonly IExpenseRepository _repository;

    public ExpenseService(IExpenseRepository repository) {
        _repository = repository;
    }

    public async Task<IEnumerable<Expense>> GetAllAsync() {
        return await _repository.GetAllAsync();
    }

    public async Task<Expense?> GetByIdAsync(int id) {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Expense expense) {
        if (expense.Amount <= 0)
            throw new ArgumentException("El importe debe ser mayot a cero.");

        await _repository.AddAsync(expense);
    }

    public async Task UpdateAsync(Expense expense)
{
    var existingExpense = await _repository.GetByIdAsync(expense.Id);

    if (existingExpense == null)
    {
        throw new KeyNotFoundException("No se encontró el gasto.");
    }

    if (expense.Amount <= 0)
    {
        throw new ArgumentException("El importe debe ser mayor a cero.");
    }

    existingExpense.Description = expense.Description;
    existingExpense.Amount = expense.Amount;
    existingExpense.Date = expense.Date;
    existingExpense.Category = expense.Category;
    existingExpense.RegisteredByUserId = expense.RegisteredByUserId;
    existingExpense.PaidByUserId = expense.PaidByUserId;
    existingExpense.GroupId = expense.GroupId;

    await _repository.UpdateAsync(existingExpense);
}
    public async Task DeleteAsync(int id) {
        var expense = await _repository.GetByIdAsync(id);

        if (expense == null)
            throw new KeyNotFoundException("No se encontró el gasto.");

        await _repository.DeleteAsync(expense);
    }
}
