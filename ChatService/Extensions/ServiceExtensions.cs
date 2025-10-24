using ChatService.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return serviceCollection;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection; 
    }
}