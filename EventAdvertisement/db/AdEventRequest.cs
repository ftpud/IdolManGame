namespace AdvertisementEvent.db;

public class AdEventRequest
{
    public int _id { get; set; }
    public int charactedId { get; set; }
    public DateTime eventDate { get; set; }
    
    public int EventType { get; set; }
}