using Vaquita.Api.Models;
using Vaquita.Api.DTOs;

namespace Vaquita.Api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> RegisterAsync(RegisterRequest request);
    Task<User?> LoginAsync(LoginRequest request);
}
