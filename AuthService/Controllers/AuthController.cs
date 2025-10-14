using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(){
        return Ok();
    }
}