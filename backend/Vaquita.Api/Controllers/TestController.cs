using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Vaquita.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class TestController : ControllerBase {
  [HttpGet]
  public IActionResult Get() {
    return Ok("La API funciona!");
  }
}
