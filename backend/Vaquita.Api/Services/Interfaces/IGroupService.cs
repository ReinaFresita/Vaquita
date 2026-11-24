using Vaquita.Api.Models;

namespace Vaquita.Api.Services.Interfaces;

public interface IGroupService
{
    Task<IEnumerable<Group>> GetAllAsync();
    Task<Group?> GetByIdAsync(int id);
    Task AddAsync(Group group);
    Task UpdateAsync(Group group);
    Task DeleteAsync(int id);
}
