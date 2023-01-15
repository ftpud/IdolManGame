using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace EventOffersProcessing.Shared.Offers.RadioOfferPages;

[Managed]
public class RadioOfferView : IView
{
    public ViewResponse Display(IState viewModel)
    {
        RadioOfferPage model = (RadioOfferPage)viewModel; 
        return new ViewSimpleResponse(@$"
<code>{model.Offer.Text}</code>
{model.Offer.Description}

{model.SingerPicker}
{model.SongPicker}

", ViewHelper.ButtonBuilder.Create()
            .AddBackButton("Назад")
            .Add("Отправить", "/continue", model.FormIsComplete).Build());
    }
}