using GameShared.UI.Elements;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace GameShared.UI.View;

[Managed]
public class UiTextDialogView : IView
{
    public ViewResponse Display(IState viewModel)
    {
        UiTextDialog testViewModel = (UiTextDialog)viewModel;
        return new ViewSimpleResponse($@"{testViewModel.Text}
{testViewModel.ErrorMessage}");
    }
}