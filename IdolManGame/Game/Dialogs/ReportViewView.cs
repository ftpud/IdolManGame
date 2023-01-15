using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.Dialogs;

[Managed]
public class ReportViewView : IView
{
    public ViewResponse Display(IState viewModel)
    {
        var model = (ReportViewDialog)viewModel;
        return new ViewSimpleResponse($"<code>{model.Offer.EventDate}</code>\n{model.Offer.Report}",
            ViewHelper.ButtonBuilder.Create()
                .AddBackButton("Назад")
                .Add("Удалить", "/delete")
                .Build()
        );
    }
}