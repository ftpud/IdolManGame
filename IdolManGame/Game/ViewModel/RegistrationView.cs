using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class RegistrationView : IView
{
    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (RegistrationPage)sharedViewModel;
        return new ViewSimpleResponse(
            $"Регистрация. \n" +
            $"{viewModel.nameTextBox}\n" +
            $"{viewModel.groupTextBox}\n",
            ViewHelper.ButtonBuilder.Create()
                .Add("Зарегистрироваться", "/reg", viewModel.IsFormReady)
                .Build());
    }
}