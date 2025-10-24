using ChatService.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) :
 DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatUserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}