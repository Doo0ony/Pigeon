using ChatService.Enums;

namespace ChatService.Models;

public class Chat
{
    public Guid Id { get; set; }
    public ChatType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<ChatUser> ChatUsers { get; set; } = [];
}