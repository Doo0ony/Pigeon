using AuthService.Models.DTOs;

namespace AuthService.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserSearchResultDto>> SearchByUserNameAsync(string userName);
}