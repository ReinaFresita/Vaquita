using Microsoft.EntityFrameworkCore;
using Vaquita.Api.Data;
using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;

namespace Vaquita.Api.Repositories;

public class GroupMemberRepository : IGroupMemberRepository
{
    private readonly VaquitaContext _context;

    public GroupMemberRepository(VaquitaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GroupMember>> GetByGroupIdAsync(int groupId)
    {
        return await _context.GroupMembers
            .Where(gm => gm.GroupId == groupId)
            .ToListAsync();
    }

    public async Task<GroupMember?> GetByGroupAndUserAsync(int groupId, int userId)
    {
        return await _context.GroupMembers
            .FirstOrDefaultAsync(gm =>
                gm.GroupId == groupId && gm.UserId == userId);
    }

    public async Task AddAsync(GroupMember member)
    {
        await _context.GroupMembers.AddAsync(member);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(GroupMember member)
    {
        _context.GroupMembers.Remove(member);
        await _context.SaveChangesAsync();
    }
}
