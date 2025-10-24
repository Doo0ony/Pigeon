
using AuthService.Models.DTOs;
using AuthService.Models.DTOs.Filters;
using Shared.Models;

namespace AuthService.Services.Interfaces;

public interface IUserService
{
    Task<ServiceResult<List<UserSearchResultDto>>> SearchUser(SearchUserFilterDto dto);
}