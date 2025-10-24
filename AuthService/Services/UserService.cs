using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Models.DTOs.Filters;
using AuthService.Repositories.Interfaces;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace AuthService.Services;

public class UserService(UserManager<ApplicationUser> userManager,
IUserRepository userRepository) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ServiceResult<List<UserSearchResultDto>>> SearchUser(SearchUserFilterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.UserName))
            return ServiceResult<List<UserSearchResultDto>>.SuccessResult([]);
            
        var result = await _userRepository.SearchByUserNameAsync(dto.UserName);

        return ServiceResult<List<UserSearchResultDto>>.SuccessResult(result);
    }
}