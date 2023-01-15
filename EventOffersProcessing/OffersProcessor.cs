using EventEngine;
using EventOffersProcessing.Shared;
using EventOffersProcessing.Shared.Entity;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.Interfaces;

namespace EventOffersProcessing;

[Managed]
[IdolEvent(dbname = "offers.db", collectionName = "offers")]
public class OffersProcessor : IUglyLoader
{
    private int DELAY { get; } = 10000;

    [Inject] private OfferManager _offerManager { get; set; }

    private int LastProcessedId = 0;
    
    public virtual void Trigger()
    {
        foreach (var offer in _offerManager.GetAllOffersPostNow())
        {
            if (offer.state == OfferState.InProcess)
            {
                _offerManager.Process(offer);
            }
            else if (offer.state == OfferState.Created)
            {
                _offerManager.DeleteOffer(offer._id);
            }
            
        }
    }

    public void Start()
    {
        while (true)
        {
            Thread.Sleep(DELAY);
            Trigger();
        }
    }

    public virtual void Load()
    {
        Start();
    }
}