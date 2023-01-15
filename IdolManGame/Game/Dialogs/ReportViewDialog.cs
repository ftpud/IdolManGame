using EventOffersProcessing.Shared;
using EventOffersProcessing.Shared.Entity;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;

namespace IdolManGame.Game.Dialogs;

[BindView(typeof(ReportViewView))]
public class ReportViewDialog : UglyTgApplication.States.ViewModel
{
    internal OfferEntity Offer { get; set; }
    [Inject] private OfferManager _offerManager { get; set; }
    
    public ReportViewDialog(OfferEntity entity)
    {
        Offer = entity;
    }
    
    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }
    
    [Callback(Trigger = "/delete")]
    public void DeleteCallback(Update update)
    {
        _offerManager.DeleteOffer(Offer._id);
        Pop();
    }
}