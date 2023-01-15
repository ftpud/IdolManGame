using EventOffersProcessing.Shared.Entity;
using EventOffersProcessing.Shared.Offers.RadioOfferPages;
using UglyAppFramework.DependencyManager.Attributes;

namespace EventOffersProcessing.Shared.Offers;

[Managed(Identifier = "TvOfferBean")]
public class TvOffer : OfferBase
{
    [Inject] private OfferManager _offerManager { get; set; }
    
    public override UglyTgApplication.States.ViewModel GetViewModel(OfferEntity e)
    {
        //e.state = 1;
        //Console.WriteLine("Tv Offer");
        //_offerManager.PushOffer(e);
        return new RadioOfferPage(e);
    }

    public override void Process(OfferEntity e)
    {
        Console.WriteLine("Вы пожрали говна");
        e.state = OfferState.Completed;
        _offerManager.PushOffer(e);
        
    }
}