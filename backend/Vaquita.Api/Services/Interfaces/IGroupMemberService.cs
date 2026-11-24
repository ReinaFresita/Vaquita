using Vaquita.Api.Models;

namespace Vaquita.Api.Services.Interfaces;

public interface IGroupMemberService
{
    Task<IEnumerable<GroupMember>> GetByGroupIdAsync(int groupId);
    Task AddMemberAsync(int groupId, int userId);
    Task RemoveMemberAsync(int groupId, int userId);
}
