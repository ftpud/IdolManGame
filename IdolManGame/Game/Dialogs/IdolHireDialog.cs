using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using WorldEngine.Game.Entity;

namespace IdolManGame.Game.Dialogs;

[BindView(typeof(IdolHireView))]
public class IdolHireDialog : UglyTgApplication.States.ViewModel
{
    internal HumanEntity Person { get; set; }
    
    [Inject] private WorldEngine.Game.Engine.WorldEngine _WorldEngine { get; set; }
    
    public IdolHireDialog(HumanEntity entity)
    {
        Person = entity;
    }
    
    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }
    
    [Callback(Trigger = "/hire")]
    public void HireCallback(Update update)
    {
        _WorldEngine.Hire(Person, Context.UserId);
        Pop();
    }
}