using AuthService.Models.DTOs;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IIdentityService IdentityService) : ControllerBase
    {
        private readonly IIdentityService _identityService = IdentityService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _identityService.RegisterAsync(dto);
            return result.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _identityService.LoginAsync(dto);
            return result.ToActionResult();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenResponseDto dto)
        {
            var result = await _identityService.RefreshTokenAsync(dto);
            return result.ToActionResult();
        }
    }
}