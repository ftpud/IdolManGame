using LiteDB;
using UglyAppFramework.Interfaces;

namespace EventEngine;

public class IdolEvent : IUglyLoader
{
    protected LiteDatabase db;
    protected IdolEventAttribute _idolEventAttribute;
    
    public IdolEvent()
    {
        IdolEventAttribute attribute = (IdolEventAttribute) Attribute.GetCustomAttribute(this.GetType(), typeof (IdolEventAttribute));
        _idolEventAttribute = attribute;
    }
    

    protected ILiteCollection<T> Initialize<T>()
    {
        db = new LiteDatabase($"Filename={_idolEventAttribute.dbname};connection=shared");
        return db.GetCollection<T>(_idolEventAttribute.collectionName);
    }

    public virtual void Trigger()
    {
    }

    public void Start()
    {
        while (true)
        {
            Thread.Sleep(_idolEventAttribute.delay);
            Trigger();
        }
    }

    public virtual void Load()
    {
        Start();
    }
}