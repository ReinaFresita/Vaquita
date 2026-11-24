using Microsoft.AspNetCore.Mvc;
using Vaquita.Api.Models;
using Vaquita.Api.Services.Interfaces;

namespace Vaquita.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service) {
        _service = service;
    }

    // GET: api/expenses
    [HttpGet]

    public async Task<IActionResult> GetAll() {
        var expenses = await _service.GetAllAsync();

        return Ok(expenses);
    }

    // GET: api/expenses/5
    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id) {
        var expense = await _service.GetByIdAsync(id);

        if (expense == null)
            return NotFound();

        return Ok(expense);
    }

    // POST: api/expenses
    [HttpPost]

    public async Task<IActionResult> Create(Expense expense) {
      try {
          await _service.AddAsync(expense);

          return CreatedAtAction(
                nameof(GetById),
                new {id = expense.Id},
                expense
              );
      }

      catch (ArgumentException ex) {
          return BadRequest(ex.Message);
      }
    }

    // PUT: api/expenses/5
    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, Expense expense) {
        if (id != expense.Id)
            return BadRequest("El ID de la URL no coincide con el ID del gasto.");

        try {
            await _service.UpdateAsync(expense);

            return NoContent();
        }

        catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }

        catch (KeyNotFoundException) {
          return NotFound();
        }
    }

    // DELETE: api/expenses/5
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
