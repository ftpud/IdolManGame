using GameShared.UI;
using GameShared.UI.Elements;
using GameShared.UI.FormElements;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using WorldEngine.Game.Entity;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(EventsView))]
public class EventsPage : UglyTgApplication.States.ViewModel
{
    internal FormButton<int> cheapEventButton { get; set; }

    [Inject] private WorldEngine.Game.Engine.WorldEngine _worldEngine { get; set; }

    [Inject] private AdvertisementEvent.AdvertisementEvent _advertisementEvent { get; set; }

    [PostConstruct]
    void Init()
    {
        cheapEventButton = new FormButton<int>(this, "Раздавать листовки на площади", i =>
        {
            var idols = _worldEngine
                .GetMyContracts(Context.UserId)
                .Select(e => (Object)e)
                .ToDictionary(e => UiHelper.HumanEntityToShortInfoConverter((HumanEntity)e));
            Push(new UiPickerDialog("Выбрать участника:", idols, (s, o) =>
                {
                    var idol = (HumanEntity)o;
                    _advertisementEvent.PlaceRequest(idol.Id, DateTime.Now, i);
                    Pop();
                }
            ));
        }, 0);
    }


    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }
}