using AdvertisementEvent.db;
using WorldEngine.Game.Entity;

namespace AdvertisementEvent.AdTypes;

public abstract class AdPerformanceBase: IAdPerformance
{
    protected WorldEngine.Game.Engine.WorldEngine _worldEngine;
    public AdPerformanceBase(WorldEngine.Game.Engine.WorldEngine engine)
    {
        _worldEngine = engine;
    }

    public virtual List<HumanEntity> GetAuditory(AdEventRequest request)
    {
        throw new NotImplementedException();
    }

    public virtual void Perform(List<HumanEntity> auditory, AdEventRequest request)
    {
        throw new NotImplementedException();
    }
}