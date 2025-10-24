
using AuthService.Data;
using AuthService.Models;
using AuthService.Repositories;
using AuthService.Repositories.Interfaces;
using AuthService.Services;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return serviceCollection;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return serviceCollection;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<TokenService>();
        
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        
        return serviceCollection;
    }
}