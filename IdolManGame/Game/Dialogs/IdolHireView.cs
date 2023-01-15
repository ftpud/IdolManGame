using GameShared.UI;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.Dialogs;

[Managed]
public class IdolHireView : IView
{
    public ViewResponse Display(IState viewModel)
    {
        var model = (IdolHireDialog)viewModel;
        return new ViewSimpleResponse(
            UiHelper.HumanEntityToIdolCardConverter(model.Person),
            ViewHelper.ButtonBuilder.Create()
                .AddBackButton("Назад")
                .Add("Нанять", "/hire")
                .Build()
        );
    }
}