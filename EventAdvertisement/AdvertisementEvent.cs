using AdvertisementEvent.AdTypes;
using AdvertisementEvent.db;
using EventEngine;
using EventEngine.Notifications;
using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;
using WorldEngine.Game.Entity;

namespace AdvertisementEvent;

[Managed]
[IdolEvent(dbname = "events.db", collectionName = "TvAds")]
public class AdvertisementEvent : IdolEvent
{
    [Inject] private WorldEngine.Game.Engine.WorldEngine _worldEngine { get; set; }
    [Inject] private NotificationMessagesManager _NotificationMessagesManager { get; set; }

    private ILiteCollection<AdEventRequest> collection;
    
    public static Random Randomizer = new Random(11111);

    private Dictionary<int, IAdPerformance> PerformancesRepo;

    [PostConstruct]
    public void Init()
    {
        collection = Initialize<AdEventRequest>(); 
        PerformancesRepo  = new Dictionary<int, IAdPerformance>()
        {
            {0, new CheapAdsPerformance(_worldEngine)}
        };
    }

    public override void Trigger()
    {
        foreach (var request in collection.Query().Where(x => x.eventDate < DateTime.Now).ToList())
        {
            Console.WriteLine($"Starting event by {request.charactedId}");
            //Console.WriteLine($"Performer {participant.Name} ({participant.IsHiredBy})");
            
            var auditory = PerformancesRepo[request.EventType].GetAuditory(request);
            PerformancesRepo[request.EventType].Perform(auditory, request);
            //ProcessRequest(request);
            collection.Delete(request._id);
            Console.WriteLine(GenerateReport(request.charactedId, auditory));
        }
    }
   
    private string GenerateReport(int entityId, List<HumanEntity> auditory)
    {
        int recognition = 0;
        int recognitionOver66 = 0;

        int totalDislike = 0;
        int totalNeutral = 0;
        int totalLike = 0;
        int totalOshi = 0;


        // _worldEngine.WorldCollection.FindAll()
        foreach (HumanEntity entity in _worldEngine.WorldCollection.FindAll())
        {
            if (entity.Recognition.ContainsKey(entityId) && entity.Recognition[entityId] > 0)
            {
                recognition++;
            }
            if (entity.Recognition.ContainsKey(entityId) && entity.Recognition[entityId] > 0.66f)
            {
                recognitionOver66++;
            }
            if (entity.Like.ContainsKey(entityId) && entity.Like[entityId] > 0.66f)
            {
                totalLike++;
            }
            if (entity.Like.ContainsKey(entityId) && entity.Like[entityId] <= 0.33f)
            {
                totalDislike++;
                
            }
            if (entity.Like.ContainsKey(entityId) && entity.Like[entityId] > 0.33f && entity.Like[entityId] <= 0.66f)
            {
                totalNeutral++;
            }
            if (entity.Oshimen == entityId)
            {
                totalOshi++;
            }
        }

        int multiplier = 1;


        return $@"
<b>Узнают</b>: {multiplier * recognition}
<b>Запомнили</b>: {multiplier * recognitionOver66}

<b>Не нравится</b>: {multiplier * totalDislike}
<b>Нейтрально</b>: {multiplier * totalNeutral}
<b>Нравится</b>: {multiplier * totalLike}

<b>Оши</b>: {multiplier * totalOshi}
";
    }


    public void PlaceRequest(int characterId, DateTime date, int type)
    {
        collection.Insert(new AdEventRequest()
        {
            charactedId = characterId,
            eventDate = date,
            EventType = type
        });
    }
}