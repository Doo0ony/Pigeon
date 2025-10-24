using AuthService.Data;
using AuthService.Models.DTOs;
using AuthService.Models.DTOs.Filters;
using AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<UserSearchResultDto>> SearchAsync(SearchUserFilterDto dto)
    {
        var users = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(dto.UserName))
        {
            users = users.Where(u =>
                u.UserName != null &&
                u.UserName.ToLower().Contains(dto.UserName.ToLower()));
        }

        var result = await users
            .Select(u => new UserSearchResultDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
            })
            .OrderBy(u => u.UserName)
            .Take(10)
            .ToListAsync();

        return result;
    }
}