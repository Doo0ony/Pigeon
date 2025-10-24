using AuthService.Models.DTOs;
using Shared.Models;

namespace AuthService.Services.Interfaces;

public interface IIdentityService
{
    Task<ServiceResult> RegisterAsync(RegisterDto dto);
    Task<ServiceResult<TokenResponseDto>> LoginAsync(LoginDto dto);
    Task<ServiceResult<TokenResponseDto>> RefreshTokenAsync(TokenResponseDto dto);
}