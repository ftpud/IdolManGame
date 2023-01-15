using EventOffersProcessing.Shared.Entity;

namespace EventOffersProcessing.Shared;

public abstract class OfferBase
{
    // public OfferEntity entity { get; set; }
    
    public abstract UglyTgApplication.States.ViewModel GetViewModel(OfferEntity e);
    
    public abstract void Process(OfferEntity e);
}