using EventOffersProcessing.Shared;
using IdolManGame.Game.Repository.UserManagement;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;
using EventOffersProcessing.Shared.Entity;
using GameShared.Services;
using GameShared.UI.FormElements;
using IdolManGame.Game.Dialogs;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(MainPageView))]
public class MainPage : UglyTgApplication.States.ViewModel
{
    [Inject] internal NotificationManager _notificationManager { get; set; }
    [Inject] private UserManager _userManager { get; set; }

    [Inject] private AdvertisementEvent.AdvertisementEvent _advertisement { get; set; }
    
    public List<FormButton<OfferEntity>> OfferList { get; set; }
    public List<String> OffersInProcess { get; set; }
    public List<FormButton<OfferEntity>> OffersDone { get; set; }

    private void UpdateOffersLists()
    {
        OfferList = _offerManager.GetAllOffersByUid(Context.UserId)
            .Where(e => e.state == OfferState.Created).Select(e =>
                new FormButton<OfferEntity>(this, $"<code>{e.Text}</code> <i>[{e.EventDate}]</i>", entity =>
                {
                    Push(_offerManager.GetViewModel(e));
                    UpdateOffersLists();
                    UpdateView();
                }, e)
            ).ToList();
        

        OffersInProcess = _offerManager.GetAllOffersByUid(Context.UserId)
            .Where(e => e.state == OfferState.InProcess).Select(e =>
                $"<i>{e.Text} (В процессе) [{e.EventDate}]</i>"
            ).ToList();
        
        OffersDone = _offerManager.GetAllOffersByUid(Context.UserId)
            .Where(e => e.state == OfferState.Completed).Select(e =>
                new FormButton<OfferEntity>(this, $"<i>{e.Text}</i>\n<b>Читать:</b>", entity =>
                {
                    Push(new ReportViewDialog(entity));
                }, e)
            ).ToList();
    }

    [Inject] private OfferManager _offerManager { get; set; }
    internal FormButton<String> pushRad;
    internal FormButton<String> pushTv;


    [PostConstruct]
    public void bInit()
    {
        pushRad = new FormButton<string>(this, "addRad", s =>
        {
            _offerManager.PushOffer(new OfferEntity()
            {
                OfferIdentifier = "RadioOfferBean",
                OfferData = "{}",
                state = 0,
                Text = "Вам поступило предложение выступить на местном Радио!",
                Description = "Блабла, описание эвента, Бла бла бла. Лол кек чебурек.",
                uid = Context.UserId,
                EventDate = DateTime.Now.AddMinutes(1)
            });
            UpdateView();
        }, "r1");
    }


    public String UserName => _userManager.GetCurrentUser(Context.CurrentUserId.Identifier.Value).Nickname;
    public String GroupName => _userManager.GetCurrentUser(Context.CurrentUserId.Identifier.Value).GroupName;

    public int Cash => _userManager.GetCurrentUser(Context.UserId).Cash;
    public int ActionPoints => _userManager.GetCurrentUser(Context.UserId).ActionsCount;

    [Callback(Trigger = "/audition")]
    public void AuditionCallback(Update update)
    {
        Push(new AuditionRequestPage());
    }

    [Callback(Trigger = "/idols")]
    public void IdolsCallback(Update update)
    {
        Push(new IdolsViewPage());
    }

    [Callback(Trigger = "/events")]
    public void EventsCallback(Update update)
    {
        Push(new EventsPage());
    }


    [Callback(Trigger = "/repertoire")]
    public void RepertoireCallback(Update update)
    {
        Push(new RepertoirePage());
    }
    
    [Callback(Trigger = "/check")]
    public void MailCallback(Update update)
    {
        UpdateOffersLists();
        UpdateView();
    }

    public override void Activate()
    {
        UpdateOffersLists();
        base.Activate();
    }

    public override void Initialize()
    {
        _notificationManager.LogCollection.CollectionChanged += (sender, args) => UpdateView();
        
        UpdateOffersLists();
        base.Initialize();
    }
}