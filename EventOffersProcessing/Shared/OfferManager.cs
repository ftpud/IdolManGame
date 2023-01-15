using EventOffersProcessing.Shared.Entity;
using GameShared.Helpers;
using LiteDB;
using UglyAppFramework.DependencyManager;
using UglyAppFramework.DependencyManager.Attributes;

namespace EventOffersProcessing.Shared;

[Managed]
public class OfferManager
{
    [Inject] private DependencyManager _dependencyManager { get; set; }

    public ILiteCollection<OfferEntity> Collection;

    [PostConstruct]
    public virtual void Init()
    {
        Collection = DbHelper.GetDbCollection<OfferEntity>("offers.db", "offers");
    }
    
    public List<OfferEntity> GetAllReportsForUid(long uid)
    {
        var response = Collection.Query().Where(e => e.uid == uid).ToList();
        return response;
    }


    public List<OfferEntity> GetAllOffersByUid(long uid)
    {
        return Collection.Query().Where(c => c.uid == uid).ToList();
    }

    public void PushOffer(OfferEntity entity)
    {
        Collection.Upsert(entity);
    }

    public UglyTgApplication.States.ViewModel GetViewModel(OfferEntity entity)
    {
        var instance = GetOfferBeanInstance(entity);
        return instance.GetViewModel(entity);
    }

    public void Process(OfferEntity entity)
    {
        var instance = GetOfferBeanInstance(entity);
        instance.Process(entity);
    }

    private OfferBase GetOfferBeanInstance(OfferEntity entity)
    {
        return (OfferBase)_dependencyManager.GetDependencyInstance(null, new InjectAttribute()
        {
            Identifier = entity.OfferIdentifier
        });
    }

    public void DeleteOffer(int id)
    {
        Collection.Delete(id);
    }


    public List<OfferEntity> GetAllProcessingOffers()
    {
        return Collection.Query().Where(c => c.state == OfferState.InProcess).ToList();
    }

    public List<OfferEntity> GetAllOffersPostNow()
    {
        return Collection.Query().Where(c => c.EventDate <= DateTime.Now).ToList();
    }
}