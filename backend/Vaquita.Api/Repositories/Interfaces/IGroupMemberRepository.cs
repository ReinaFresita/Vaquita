using Vaquita.Api.Models;

namespace Vaquita.Api.Repositories.Interfaces;

public interface IGroupMemberRepository
{
    Task<IEnumerable<GroupMember>> GetByGroupIdAsync(int groupId);
    Task<GroupMember?> GetByGroupAndUserAsync(int groupId, int userId);
    Task AddAsync(GroupMember member);
    Task DeleteAsync(GroupMember member);
}
