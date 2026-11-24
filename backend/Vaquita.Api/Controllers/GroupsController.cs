using Microsoft.AspNetCore.Mvc;
using Vaquita.Api.Models;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GroupsController : ControllerBase
{
    private readonly IGroupService _service;

    public GroupsController(IGroupService service) {
        _service = service;
    }

    // GET: api/groups
    [HttpGet]

    public async Task<IActionResult> GetAll() {
        var groups = await _service.GetAllAsync();

        return Ok(groups);
    }

    // GET: api/groups/5
    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id) {
        var group = await _service.GetByIdAsync(id);

        if (group == null)
            return NotFound();

        return Ok(group);
    }

    // POST: api/groups
    [HttpPost]

    public async Task<IActionResult> Create(Group group) {
        try {
            await _service.AddAsync(group);

            return CreatedAtAction(
                  nameof(GetById),
                  new {id = group.Id},
                  group
                );
        }

        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/groups/5
    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, Group group) {
        if (id != group.Id)
            return BadRequest("El ID de la URL no coincide con el ID del grupo.");

        try {
            await _service.UpdateAsync(group);

            return NoContent();
        }

        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }

        catch (KeyNotFoundException) {
            return NotFound();
        }
    }

    // DELETE: api/groups/5
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id) {
        try {
          await _service.DeleteAsync(id);

          return NoContent();
        }

        catch (KeyNotFoundException) {
            return NotFound();
        }
    }
}

