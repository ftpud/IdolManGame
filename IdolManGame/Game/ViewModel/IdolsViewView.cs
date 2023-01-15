using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class IdolsViewView : IView
{
    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (IdolsViewPage)sharedViewModel;
        return new ViewSimpleResponse($@"
Ваши айдолы:

{String.Join("\n", viewModel.idolCards)}

", ViewHelper.ButtonBuilder.BuildBackButton()
        );
    }
}