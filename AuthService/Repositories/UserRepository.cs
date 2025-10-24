using AuthService.Data;
using AuthService.Models;
using AuthService.Models.DTOs;
using AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<UserSearchResultDto>> SearchByUserNameAsync(string userName)
    {
        var users = await _context.Users
            .Where(u => u.UserName != null && u.UserName.ToLower().Contains(userName.ToLower()))
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

        return users;
    }
}