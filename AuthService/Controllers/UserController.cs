using AuthService.Models.DTOs.Filters;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace AuthService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) 
{

    private readonly IUserService _userService = userService;

    [HttpPost("search")]
    public async Task<IActionResult> SearchUser([FromBody] SearchUserFilterDto dto)
    {
        var result = await _userService.SearchUserAsync(dto);

        return result.ToActionResult();
    }

    [HttpPost("exists")]
    public async Task<IActionResult> UserExists([FromBody] IEnumerable<string> userIds)
    {
        var result = await _userService.UserExistsAsync(userIds);

        return result.ToActionResult();
    }
}