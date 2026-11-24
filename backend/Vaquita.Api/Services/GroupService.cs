using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _repository;

    public GroupService(IGroupRepository repository) {
        _repository = repository;
    }

    public async Task<IEnumerable<Group>> GetAllAsync() {
        return await _repository.GetAllAsync();
    }

    public async Task<Group?> GetByIdAsync(int id) {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Group group) {
        if (string.IsNullOrWhiteSpace(group.Description))
            throw new ArgumentException("La descripción del grupo es obligatoria.");

        await _repository.AddAsync(group);
    }

    public async Task UpdateAsync(Group group)
{
    if (string.IsNullOrWhiteSpace(group.Description))
        throw new ArgumentException("La descripción del grupo es obligatoria.");

    var existingGroup = await _repository.GetByIdAsync(group.Id);

    if (existingGroup == null)
        throw new KeyNotFoundException("No se encontró el grupo.");

    existingGroup.Description = group.Description;

    await _repository.UpdateAsync(existingGroup);
}

    public async Task DeleteAsync(int id) {
        var group = await _repository.GetByIdAsync(id);

        if (group == null)
            throw new KeyNotFoundException("No se encontró el grupo.");

        await _repository.DeleteAsync(group);
    }
}
