using Microsoft.EntityFrameworkCore;
using Vaquita.Api.Data;
using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;

namespace Vaquita.Api.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly VaquitaContext _context;

    public GroupRepository(VaquitaContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Group>> GetAllAsync() {
        return await _context.Groups.ToListAsync();
    }

    public async Task<Group?> GetByIdAsync(int id) {
        return await _context.Groups
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task AddAsync(Group group) {
        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Group group) {
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Group group) {
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }
}
