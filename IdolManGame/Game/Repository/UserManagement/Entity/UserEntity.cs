namespace IdolManGame.Game.Repository.UserManagement.Entity;

public class UserEntity
{
    public long _id { get; set; }
    public long TelegramId { get; set; }
    public long ChatId { get; set; }
    public String Nickname { get; set; }
    public String GroupName { get; set; }
    public int Cash { get; set; }
    
    public int ActionsCount { get; set; }
}