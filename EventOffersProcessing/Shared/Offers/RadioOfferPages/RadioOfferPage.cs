using EventOffersProcessing.Shared.Entity;
using GameShared.Services;
using GameShared.Services.Entity;
using GameShared.UI.FormElements;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using WorldEngine.Game.Entity;

namespace EventOffersProcessing.Shared.Offers.RadioOfferPages;

[BindView(typeof(RadioOfferView))]
public class RadioOfferPage : UglyTgApplication.States.ViewModel
{
    public OfferEntity Offer { get; set; }

    internal HumanEntity Singer { get; set; }
    internal SongEntity Song { get; set; }

    [Inject] internal WorldEngine.Game.Engine.WorldEngine _worldEngine { get; set; }
    [Inject] internal SongManager _SongManager { get; set; }

    [Inject] internal OfferManager _OfferManager { get; set; }

    internal bool FormIsComplete => Singer != null && Song != null;

    internal FormPickerButton<HumanEntity> SingerPicker { get; set; }
    internal FormPickerButton<SongEntity> SongPicker { get; set; }

    public RadioOfferPage(OfferEntity offer)
    {
        Offer = offer;
    }

    public override void Initialize()
    {
        SingerPicker = new FormPickerButton<HumanEntity>(this, "Выберите исполнителя", entity =>
            {
                Singer = entity;
                UpdateView();
            },
            _worldEngine.GetMyContracts(Context.UserId).ToDictionary(i => i.Name, i => i)
        );

        SongPicker = new FormPickerButton<SongEntity>(this, "Выберите Композицию", entity =>
            {
                Song = entity;
                UpdateView();
            },
            _SongManager.GetAllSongsByUid(Context.UserId).ToDictionary(i => i.SongName, i => i)
        );
        base.Initialize();
    }

    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }


    [Callback(Trigger = "/continue")]
    public void ContinueCallback(Update update)
    {
        Offer.OfferData = JsonConvert.SerializeObject(
            new RadioOfferEntity()
            {
                SingerId = Singer.Id,
                SongId = Song._id
            }
        );
        Offer.state = OfferState.InProcess;
        _OfferManager.PushOffer(Offer);
        Pop();
    }
}