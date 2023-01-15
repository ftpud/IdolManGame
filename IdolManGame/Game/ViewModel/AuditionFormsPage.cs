using AuditionEvent;
using GameShared.Services;
using GameShared.UI;
using GameShared.UI.FormElements;
using IdolManGame.Game.Dialogs;
using IdolManGame.Game.Repository.UserManagement;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using WorldEngine.Game.Entity;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(AuditionFormsView))]
public class AuditionFormsPage : UglyTgApplication.States.ViewModel
{
    [Inject] private UserManager _userManager { get; set; }
    [Inject] private NotificationManager _notificationManager { get; set; }
    [Inject] private AuditionEventManager _auditionEventManager { get; set; }

    // internal List<HumanEntity> responses { get; set; }

    internal List<FormButton<HumanEntity>> auditionResponses = new List<FormButton<HumanEntity>>();

    [Callback(Trigger = "/back")]
    public void RegisterCallback(Update update)
    {
        Pop();
    }

    public override void Initialize()
    {
        UpdateList();

        base.Initialize();
    }

    private void UpdateList()
    {
        auditionResponses.Clear();
        var responses = _auditionEventManager.CheckResponses(Context.UserId);
        foreach (var humanEntity in responses)
        {
            if (humanEntity.IsHiredBy == 0)
            {
                auditionResponses.Add(new FormButton<HumanEntity>(this,
                    $"<code>{UiHelper.HumanEntityToShortInfoConverter(humanEntity)}</code>\n - Подробнее ",
                    entity => Push(new IdolHireDialog(entity)),
                    humanEntity
                ));
            }
        }
    }

    public override void Activate()
    {
        UpdateList();
        UpdateView();
        base.Activate();
    }
}