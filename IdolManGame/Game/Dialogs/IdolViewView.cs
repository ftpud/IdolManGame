using GameShared.UI;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.Dialogs;

[Managed]
public class IdolViewView : IView
{
    public ViewResponse Display(IState viewModel)
    {
        var model = (IdolViewDialog)viewModel;
        return new ViewSimpleResponse(
            UiHelper.HumanEntityToIdolCardConverter(model.Person),
            ViewHelper.ButtonBuilder.Create()
                .AddBackButton("Назад")
                .Build()
        );
    }
}