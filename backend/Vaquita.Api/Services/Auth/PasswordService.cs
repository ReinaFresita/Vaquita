using Microsoft.AspNetCore.Identity;
using Vaquita.Api.Models;

namespace Vaquita.Api.Services.Auth;

public class PasswordService
{
    private readonly PasswordHasher<User> _hasher;

    public PasswordService()
    {
        _hasher = new PasswordHasher<User>();
    }

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password)
    {
        var result = _hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password
        );

        return result != PasswordVerificationResult.Failed;
    }
}
