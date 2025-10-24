using AuthService.Models.DTOs;
using AuthService.Models.DTOs.Filters;

namespace AuthService.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserSearchResultDto>> SearchAsync(SearchUserFilterDto dto);
}