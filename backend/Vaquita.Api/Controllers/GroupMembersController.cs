using Microsoft.AspNetCore.Mvc;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Controllers;

[ApiController]
[Route("api/groups/{groupId}/members")]
public class GroupMembersController : ControllerBase
{
    private readonly IGroupMemberService _service;

    public GroupMembersController(IGroupMemberService service)
    {
        _service = service;
    }

    // GET: api/groups/1/members
    [HttpGet]
    public async Task<IActionResult> GetByGroupId(int groupId)
    {
        try
        {
            var members = await _service.GetByGroupIdAsync(groupId);

            return Ok(members);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No se encontró el grupo.");
        }
    }

    // POST: api/groups/1/members/5
    [HttpPost("{userId}")]
    public async Task<IActionResult> AddMember(int groupId, int userId)
    {
        try
        {
            await _service.AddMemberAsync(groupId, userId);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/groups/1/members/5
    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveMember(int groupId, int userId)
    {
        try
        {
            await _service.RemoveMemberAsync(groupId, userId);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
