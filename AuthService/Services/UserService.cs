using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Models.DTOs.Filters;
using AuthService.Repositories.Interfaces;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace AuthService.Services;

public class UserService(UserManager<ApplicationUser> userManager,
IUserRepository userRepository) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ServiceResult<List<UserSearchResultDto>>> SearchUserAsync(SearchUserFilterDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.UserName))
        {
            var result = await _userRepository.SearchAsync(dto);
            return ServiceResult<List<UserSearchResultDto>>.SuccessResult(result);
        }

        return ServiceResult<List<UserSearchResultDto>>.SuccessResult([]);
    }

    public async Task<ServiceResult<UserExistResponseDto>> UserExistsAsync(UserExistRequestDto dto)
    {
        var userIds = dto.UserIds;
        
        if (userIds is null || ! userIds.Any())
            return ServiceResult<UserExistResponseDto>.FailResult(
                "User list is empty",
                Shared.Enums.ErrorCode.ValidationError
            );

        var ids = userIds.ToList();

        var existingIds = await _userManager.Users
            .Where(u => ids.Contains(u.Id))
            .Select(u => u.Id)
            .ToListAsync();

        if (!existingIds.Any())
            return ServiceResult<UserExistResponseDto>.FailResult(
                "No users found",
                Shared.Enums.ErrorCode.NotFound
            );

        var result = new UserExistResponseDto
        {
            ExistingUserIds = existingIds
        };

        return ServiceResult<UserExistResponseDto>.SuccessResult(result);
    }
}