using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services
{
    public class TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
    {
        private readonly IConfiguration _config = config;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(15),
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}