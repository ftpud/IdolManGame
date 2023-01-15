using System.Collections.ObjectModel;
using UglyAppFramework.DependencyManager.Attributes;

namespace GameShared.Services;

[Managed]
public class NotificationManager
{
    public ObservableCollection<String> LogCollection { get; set; } = new ObservableCollection<string>();

    public void PlaceNotification(String notification)
    {
        LogCollection.Add(notification);
    }
}