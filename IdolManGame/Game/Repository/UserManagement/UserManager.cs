using IdolManGame.Game.Repository.UserManagement.Entity;
using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;

namespace IdolManGame.Game.Repository.UserManagement;

[Managed]
public class UserManager
{
    protected LiteDatabase db;
    protected ILiteCollection<UserEntity> userCollection;

    public bool isRegistered(long tgId)
    {
        return userCollection.FindById(tgId) != null;
    }

    public UserManager()
    {
        Load();
    }

    public void Load()
    {
        db = new LiteDatabase($"Filename=users.db;connection=shared");
        userCollection = db.GetCollection<UserEntity>("users");
    }

    public void Save(UserEntity entity)
    {
        userCollection.Upsert(entity);
    }

    public void Register(UserEntity entity)
    {
        Save(entity);
    }

    public UserEntity GetCurrentUser(long tgId)
    {
        return userCollection.FindById(tgId);
    }
    
}