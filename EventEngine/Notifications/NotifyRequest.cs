namespace EventEngine.Notifications;

public class NotifyRequest
{
    public int _id { get; set; }
    public long userId { get; set; }
    public string Message { get; set; }
}