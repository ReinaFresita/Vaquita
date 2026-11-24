using Vaquita.Api.Models;
using Vaquita.Api.Repositories.Interfaces;
using Vaquita.Api.Services.Interfaces;
using Vaquita.Api.DTOs;
using Vaquita.Api.Services.Auth;

namespace Vaquita.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly PasswordService _passwordService;

    public UserService(
       IUserRepository repository,
       PasswordService passwordService)
    {
            _repository = repository;
            _passwordService = passwordService;
    }

    public async Task<IEnumerable<User>> GetAllAsync() {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id) {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email) {
        return await _repository.GetByEmailAsync(email);
    }

    public async Task AddAsync(User user) {
        var existingUser = await _repository.GetByEmailAsync(user.Email);

        if (existingUser != null)
            throw new ArgumentException("Ya existe un usuario con ese email.");

        await _repository.AddAsync(user);
    }

   public async Task UpdateAsync(User user)
{
    var existingUser = await _repository.GetByIdAsync(user.Id);

    if (existingUser == null)
        throw new KeyNotFoundException("No se encontró el usuario.");

    var userWithEmail = await _repository.GetByEmailAsync(user.Email);

    if (userWithEmail != null && userWithEmail.Id != user.Id)
        throw new ArgumentException("Ese email ya pertenece a otro usuario.");

    existingUser.Name = user.Name;
    existingUser.Surname = user.Surname;
    existingUser.Nickname = user.Nickname;
    existingUser.Email = user.Email;

    await _repository.UpdateAsync(existingUser);
}
    public async Task DeleteAsync(int id) {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            throw new KeyNotFoundException("No se encontró el usuario.");

        await _repository.DeleteAsync(user);
    }

    public async Task<User> RegisterAsync(RegisterRequest request)
{
    if (string.IsNullOrWhiteSpace(request.Password))
        throw new ArgumentException("La contraseña es obligatoria.");

    var existingUser = await _repository.GetByEmailAsync(request.Email);

    if (existingUser != null)
        throw new ArgumentException("Ya existe un usuario con ese email.");

    var user = new User
    {
        Name = request.Name,
        Surname = request.Surname,
        Nickname = request.Nickname,
        Email = request.Email
    };

    user.PasswordHash = _passwordService.HashPassword(
        user,
        request.Password
    );

    await _repository.AddAsync(user);

    return user;
}

public async Task<User?> LoginAsync(LoginRequest request)
{
    if (string.IsNullOrWhiteSpace(request.Email) ||
        string.IsNullOrWhiteSpace(request.Password))
    {
        return null;
    }

    var user = await _repository.GetByEmailAsync(request.Email);

    if (user == null)
        return null;

    var passwordIsValid = _passwordService.VerifyPassword(
        user,
        request.Password
    );

    if (!passwordIsValid)
        return null;

    return user;
}

}
