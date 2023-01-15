using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;
using WorldEngine.Game.Entity;
using WorldEngine.Game.Repository;

namespace WorldEngine.Game.Engine;

[Managed]
public class WorldEngine
{
    public int WorldSize { get; } = 1_000_000;

    private LiteDatabase worldDatabase;
    private ILiteCollection<HumanEntity> worldCollection;

    public ILiteCollection<HumanEntity> WorldCollection => worldCollection;

    [PostConstruct]
    public void Init()
    {
        worldDatabase = new LiteDatabase(@"Filename=MyWorld.db;connection=shared");
        worldCollection = worldDatabase.GetCollection<HumanEntity>("world");

        if (worldCollection.Count() == 0)
        {
            HumanGenerator gen = new HumanGenerator();
            var hum = gen.CreateHumanEntities(WorldSize);
            worldCollection.InsertBulk(hum);
            worldCollection.EnsureIndex(e => e.IsHiredBy);
        }
    }

    private Dictionary<long, List<HumanEntity>> auditionCache = new Dictionary<long, List<HumanEntity>>();

    public Random rnd = new Random(123456);

    public List<HumanEntity> GetAuditionParticipants(long tgId, int size = 5)
    {
        if (auditionCache.ContainsKey(tgId))
        {
            return auditionCache[tgId];
        }

        List<HumanEntity> returnList;
        var q = worldCollection.Query().Where(e => e.IsHiredBy == 0 && e.OwnProperties.Age < 25).ToList();
        var qCount = q.Count();

        var ret = new List<HumanEntity>(); // q.OrderBy(i => rnd.Next()).ToList().GetRange(0, size);

        for (int i = 0; i < size; i++)
        {
            ret.Add(q[rnd.Next(0, q.Count)]);
        }

        auditionCache[tgId] = ret;
        return ret;
    }

    public void Hire(HumanEntity idol, long id)
    {
        HumanEntity ent = worldCollection.FindById(idol.Id);
        if (ent.IsHiredBy == 0)
        {
            ent.IsHiredBy = id;
            worldCollection.Update(ent);
        }
    }


    public List<HumanEntity> GetMyContracts(long tgId)
    {
        return worldCollection.Query().Where(e => e.IsHiredBy == tgId).ToList();
    }

    public HumanEntity GetHumanById(int id)
    {
        return worldCollection.FindById(id);
    }


    private List<HumanEntity> bulk = new List<HumanEntity>();
    private int bulkMaxCount = 100;

    public void BulkUpdate(HumanEntity e)
    {
        bulk.Add(e);
        if (bulk.Count >= bulkMaxCount)
        {
            worldCollection.Update(bulk);
            bulk.Clear();
        }
        
    }

    public void BulkFinalize()
    {
        if (bulk.Count > 0)
        {
            worldCollection.Update(bulk);
            bulk.Clear();
        }
    }
}