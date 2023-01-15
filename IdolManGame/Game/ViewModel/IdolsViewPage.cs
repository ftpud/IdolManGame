using GameShared.UI;
using GameShared.UI.FormElements;
using IdolManGame.Game.Dialogs;
using IdolManGame.Game.Repository.UserManagement;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using WorldEngine.Game.Entity;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(IdolsViewView))]
public class IdolsViewPage : UglyTgApplication.States.ViewModel
{
    [Inject] private UserManager _userManager { get; set; }
    [Inject] private WorldEngine.Game.Engine.WorldEngine _WorldEngine { get; set; }

    internal List<FormButton<HumanEntity>> idolCards = new List<FormButton<HumanEntity>>();

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
        idolCards.Clear();
        var responses = _WorldEngine.GetMyContracts(Context.UserId);
        foreach (var humanEntity in responses)
        {
            idolCards.Add(new FormButton<HumanEntity>(this,
                $"<code>{UiHelper.HumanEntityToShortInfoConverter(humanEntity)}</code>\n - Подробнее ",
                entity => Push(new IdolViewDialog(entity)),
                humanEntity
            ));
        }
    }
}