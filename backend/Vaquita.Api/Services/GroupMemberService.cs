using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Services;

public class GroupMemberService : IGroupMemberService
{
    private readonly IGroupMemberRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public GroupMemberService(
        IGroupMemberRepository repository,
        IUserRepository userRepository,
        IGroupRepository groupRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<IEnumerable<GroupMember>> GetByGroupIdAsync(int groupId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new KeyNotFoundException("No se encontró el grupo.");

        return await _repository.GetByGroupIdAsync(groupId);
    }

    public async Task AddMemberAsync(int groupId, int userId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId);

        if (group == null)
            throw new KeyNotFoundException("No se encontró el grupo.");

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new KeyNotFoundException("No se encontró el usuario.");

        var existingMember = await _repository
            .GetByGroupAndUserAsync(groupId, userId);

        if (existingMember != null)
            throw new ArgumentException("El usuario ya pertenece a este grupo.");

        var member = new GroupMember
        {
            GroupId = groupId,
            UserId = userId
        };

        await _repository.AddAsync(member);
    }

    public async Task RemoveMemberAsync(int groupId, int userId)
    {
        var member = await _repository
            .GetByGroupAndUserAsync(groupId, userId);

        if (member == null)
            throw new KeyNotFoundException("No se encontró ese integrante en el grupo.");

        await _repository.DeleteAsync(member);
    }
}
