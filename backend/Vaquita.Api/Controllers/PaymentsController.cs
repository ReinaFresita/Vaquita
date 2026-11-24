using Microsoft.AspNetCore.Mvc;
using Vaquita.Api.Models;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Controllers;

[ApiController]
[Route("api/groups/{groupId}/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentsController(IPaymentService service)
    {
        _service = service;
    }

    // GET: api/groups/1/payments
    [HttpGet]
    public async Task<IActionResult> GetByGroupId(int groupId)
    {
        try
        {
            var payments = await _service.GetByGroupIdAsync(groupId);

            return Ok(payments);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No se encontró el grupo.");
        }
    }

    // POST: api/groups/1/payments
    [HttpPost]
    public async Task<IActionResult> Create(int groupId, Payment payment)
    {
        try
        {
            await _service.AddAsync(groupId, payment);

            return StatusCode(201, payment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
