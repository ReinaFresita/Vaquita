using Microsoft.AspNetCore.Mvc;
using Vaquita.Api.DTOs;
using Vaquita.Api.Services.Auth;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtService _jwtService;

    public AuthController(
        IUserService userService,
        JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userService.LoginAsync(request);

        if (user == null)
            return Unauthorized("Email o contraseña incorrectos.");

        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            token,
            user = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Nickname = user.Nickname,
                Email = user.Email
            }
        });
    }
}
