using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using WorldEngine.Game.Entity;

namespace IdolManGame.Game.Dialogs;

[BindView(typeof(IdolViewView))]
public class IdolViewDialog : UglyTgApplication.States.ViewModel
{
    internal HumanEntity Person { get; set; }
    
    [Inject] private WorldEngine.Game.Engine.WorldEngine _WorldEngine { get; set; }
    
    public IdolViewDialog(HumanEntity entity)
    {
        Person = entity;
    }
    
    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }
}