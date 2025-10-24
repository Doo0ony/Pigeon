namespace ChatService.Models;

public class ChatUser
{
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
    public Guid UserId { get; set; }
    public string? Role { get; set; } = string.Empty;
}