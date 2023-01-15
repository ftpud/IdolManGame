using AuditionEvent.db;
using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;
using WorldEngine.Game.Entity;

namespace AuditionEvent;

[Managed]
public class AuditionEventManager
{
    [Inject] private WorldEngine.Game.Engine.WorldEngine WorldEngine { get; set; }
    
    private LiteDatabase auditionsDb;

    private ILiteCollection<AuditionRequest> auditionRequests;
    private ILiteCollection<AuditionResponse> auditionResponses;

    public AuditionEventManager()
    {
        Initialize();
    }

    public void Initialize()
    {
        auditionsDb = new LiteDatabase(@"Filename=auditions.db;connection=shared");
        auditionRequests = auditionsDb.GetCollection<AuditionRequest>("requests");
        auditionResponses = auditionsDb.GetCollection<AuditionResponse>("responses");
    }

    public void PlaceRequest(long userId)
    {
        auditionRequests.Insert(new AuditionRequest()
        {
            userId = userId
        });
    }

    public List<HumanEntity> CheckResponses(long userId)
    {
        var responses = auditionResponses.Query()
            .Where(r => r.userId == userId);
        if (responses.Exists())
        {
            return responses
                .ToList()
                .Select(r => WorldEngine.GetHumanById(r.characterId))
                .ToList();
        }
        else
        {
            return new List<HumanEntity>();
        }
    }

    private Random rnd = new Random((int)DateTime.Now.Ticks);

    public void StartListening()
    {
        Console.WriteLine("Listening started...");
        while (true)
        {
            Thread.Sleep(5000);
            foreach (var auditionRequest in auditionRequests.FindAll())
            {
                Console.WriteLine("Audition request found");

                auditionRequests.Delete(auditionRequest._id);

                var oldResponses = auditionResponses.Query()
                    .Where(r => r.userId == auditionRequest.userId).ToEnumerable();
                foreach (var response in oldResponses)
                {
                    auditionResponses.Delete(response._id);
                }
                
                for (int i = 0; i < 5; i++)
                {
                    auditionResponses.Insert(new AuditionResponse()
                    {
                        userId = auditionRequest.userId,
                        characterId = rnd.Next(0, WorldEngine.WorldSize)
                    });
                }
            }
        }
    }
}