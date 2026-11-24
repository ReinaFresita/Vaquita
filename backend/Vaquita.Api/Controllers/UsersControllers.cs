using Microsoft.AspNetCore.Mvc;
using Vaquita.Api.DTOs;
using Vaquita.Api.Models;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllAsync();

        var response = users.Select(user => new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Nickname = user.Nickname,
            Email = user.Email
        });

        return Ok(response);
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        var response = new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Nickname = user.Nickname,
            Email = user.Email
        };

        return Ok(response);
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> Create(RegisterRequest request)
    {
        try
        {
            var user = await _service.RegisterAsync(request);

            var response = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Nickname = user.Nickname,
                Email = user.Email
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = user.Id },
                response
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.Id)
            return BadRequest("El ID de la URL no coincide con el ID del usuario.");

        try
        {
            await _service.UpdateAsync(user);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
