using System.Security.Claims;
using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using Shared.Models;

namespace AuthService.Services;

public class IdentityService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    TokenService tokenService) : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly TokenService _tokenService = tokenService;
    private static readonly Dictionary<string, string> _refreshTokens = [];

    public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.UserName);
        if (userExists is not null)
            return ServiceResult.FailResult("User already exists", ErrorCode.ValidationError);

        var emailExists = await _userManager.FindByEmailAsync(dto.Email);
        if (emailExists is not null)
            return ServiceResult.FailResult("Email is already taken", ErrorCode.ValidationError);

        var user = new ApplicationUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return ServiceResult.FailResult(result.Errors.First().Description, ErrorCode.InternalError);

        return ServiceResult.SuccessResult();
    }

    public async Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user is null)
            return ServiceResult<TokenResponseDto>.FailResult("Invalid username or password", ErrorCode.Unauthorized);

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return ServiceResult<TokenResponseDto>.FailResult("Invalid username or password", ErrorCode.Unauthorized);

        var accessToken = await _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        _refreshTokens[user.Id] = refreshToken;

        return ServiceResult<TokenResponseDto>.SuccessResult(new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15)
        });
    }

    public async Task<ServiceResult<TokenResponseDto>> RefreshTokenAsync(TokenResponseDto dto)
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(dto.AccessToken);
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId is null || !_refreshTokens.TryGetValue(userId, out var storedToken))
            return ServiceResult<TokenResponseDto>.FailResult("Invalid refresh token", ErrorCode.Unauthorized);

        if (storedToken != dto.RefreshToken)
            return ServiceResult<TokenResponseDto>.FailResult("Invalid refresh token", ErrorCode.Unauthorized);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ServiceResult<TokenResponseDto>.FailResult("User is not found", ErrorCode.Unauthorized);

        var newAccessToken = await _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        _refreshTokens[user.Id] = newRefreshToken;

        return ServiceResult<TokenResponseDto>.SuccessResult(new TokenResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15)
        });
    }
}