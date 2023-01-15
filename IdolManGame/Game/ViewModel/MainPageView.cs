using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class MainPageView : IView
{
    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (MainPage)sharedViewModel;
        return new ViewSimpleResponse(
            @$"
<code>{viewModel.GroupName}</code>
produced by <i>{viewModel.UserName}</i>
----------------------------------
Деньги: {viewModel.Cash}$
Очки действий: {viewModel.ActionPoints}
Популярность: ???
----------------------------------
Почта: /check{GetOffers(viewModel)}
----------------------------------
Управление:
Айдолы /idols
Прослушивание /audition
Юниты
Мероприятия /events
Репертуар /repertoire
----------------------------------
Отчеты:
Исследования

Лог:
{String.Join("\n", viewModel._notificationManager.LogCollection)}

Debug/Admin:
{viewModel.pushRad}

"
        );
    }

    private string GetOffers(MainPage viewModel)
    {
        String responseStr = "";
        var offerList = viewModel.OfferList;
        var offersInProcess = viewModel.OffersInProcess;
        var offersDone = viewModel.OffersDone;
        if (offerList.Count > 0 || offersInProcess.Count > 0 || offersDone.Count > 0)
        {
            //responseStr += "\n----------------------------------";

            if (offerList.Count > 0)
            {
                responseStr += $"\n{String.Join("\n", offerList)}";
            }
            
            if (offersInProcess.Count > 0)
            {
                responseStr += $"\n{String.Join("\n", offersInProcess)}";
            }
            
            if (offersDone.Count > 0)
            {
                responseStr += $"\n{String.Join("\n", offersDone)}";
            }

        }

        return responseStr;
    }
}