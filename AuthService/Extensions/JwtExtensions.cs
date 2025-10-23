using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Extensions;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection,
     IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"] ?? throw new Exception("JWT key not set");
        var issuer = configuration["Jwt:Issuer"] ?? throw new Exception("Issuer is not set");
        var audience = configuration.GetSection("Jwt:Audience").Get<string[]>();
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudiences = audience,
                ValidIssuer = issuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };
        });

        return serviceCollection;
    }
}