using AuditionEvent;
using GameShared.Services;
using IdolManGame.Game.Repository.UserManagement;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(AuditionRequestView))]
public class AuditionRequestPage : UglyTgApplication.States.ViewModel
{
    [Inject] private UserManager _userManager { get; set; }
    [Inject] private NotificationManager _notificationManager { get; set; }
    [Inject] private AuditionEventManager _auditionEventManager { get; set; }
    
    
    [Callback(Trigger = "/back")]
    public void RegisterCallback(Update update)
    {
        Pop();
    }
    
    [Callback(Trigger = "/start")]
    public void StartCallback(Update update)
    {
        _auditionEventManager.PlaceRequest(Context.CurrentUserId.Identifier.Value);
        _notificationManager.PlaceNotification($"- Идёт подготовка к прослушиванию. Ожидайте новостей... ");
        Pop();
    }
    
    [Callback(Trigger = "/forms")]
    public void FormsCallback(Update update)
    {
        Push(new AuditionFormsPage());
    }
    
}