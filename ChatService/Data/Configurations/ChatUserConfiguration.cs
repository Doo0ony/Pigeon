using ChatService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Data.Configurations;

public class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.HasKey(cu => new { cu.ChatId, cu.UserId });

        builder.HasOne(cu => cu.Chat)
            .WithMany(c => c.ChatUsers)
            .HasForeignKey(cu => cu.ChatId);
    }
}