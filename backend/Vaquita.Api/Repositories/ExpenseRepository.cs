using Vaquita.Api.Data;
using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Vaquita.Api.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly VaquitaContext _context;

    public ExpenseRepository(VaquitaContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Expense>> GetAllAsync() {
        return await _context.Expenses.ToListAsync();
    }

    public async Task<Expense?> GetByIdAsync(int id) {
        return await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Expense expense) {
        await _context.Expenses.AddAsync(expense);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Expense expense) {
        _context.Expenses.Update(expense);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expense expense) {
        _context.Expenses.Remove(expense);

        await _context.SaveChangesAsync();
    }
}
