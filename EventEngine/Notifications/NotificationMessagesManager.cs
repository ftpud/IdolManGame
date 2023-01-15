using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;

namespace EventEngine.Notifications;

[Managed]
[IdolEvent(dbname = "events.db", collectionName = "Notify", delay = 60*60*1000)]
public class NotificationMessagesManager : IdolEvent
{
    
    private ILiteCollection<NotifyRequest> collection;


    [PostConstruct]
    public void Init()
    {
        collection = Initialize<NotifyRequest>();
    }
    

    public override void Trigger()
    {
        
    }

    public void PlaceMessage(long uid, String text)
    {
        collection.Insert(new NotifyRequest()
        {
            userId = uid,
            Message = text
        });
    }

}