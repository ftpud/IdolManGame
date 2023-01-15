using AdvertisementEvent.db;
using WorldEngine.Game.Entity;

namespace AdvertisementEvent.AdTypes;

public interface IAdPerformance
{
    public List<HumanEntity> GetAuditory(AdEventRequest request);
    public void Perform(List<HumanEntity> auditory, AdEventRequest request);
}