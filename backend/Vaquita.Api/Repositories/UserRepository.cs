using Microsoft.EntityFrameworkCore;
using Vaquita.Api.Data;
using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;

namespace Vaquita.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly VaquitaContext _context;

    public UserRepository(VaquitaContext context) {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync() {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id) {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email) {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user) {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user) {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user) {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
