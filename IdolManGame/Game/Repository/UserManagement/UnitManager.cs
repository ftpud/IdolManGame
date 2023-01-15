using IdolManGame.Game.Repository.UserManagement.Entity;
using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;

namespace IdolManGame.Game.Repository.UserManagement;

[Managed]
public class UnitManager
{
    protected LiteDatabase db;
    protected ILiteCollection<UnitEntity> collection;

    public UnitManager()
    {
        db = new LiteDatabase($"Filename=users.db;connection=shared");
        collection = db.GetCollection<UnitEntity>("units");
    }

    public List<UnitEntity> GetMyUnits(long uid)
    {
        return collection.Query().Where(u => u.OwnerId == uid).ToList();
    }

    public UnitEntity GetUnitById(int id)
    {
        return collection.FindById(id);
    }

    public void SaveUnit(UnitEntity e)
    {
        collection.Upsert(e);
        collection.EnsureIndex(u => u.OwnerId);
    }

}