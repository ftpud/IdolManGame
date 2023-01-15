namespace EventOffersProcessing.Shared.Entity;

public class OfferEntity
{
    public int _id { get; set; }

    public long uid { get; set; }
    
    public string OfferData { get; set; }
    
    public OfferState state { get; set; }
    
    public String Text { get; set; }
    
    public String Description { get; set; }
    
    public string OfferIdentifier { get; set; }
    
    public DateTime EventDate { get; set; }
    
    public String Report { get; set; }
    
}

public enum OfferState
{
    Created = 0,
    InProcess = 1,
    Completed = 10
}