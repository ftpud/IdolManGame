using IdolManGame.Game.Repository.UserManagement;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class WelcomeView : IView
{
    [Inject] private UserManager _userManager { get; set; }
    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (WelcomePage)sharedViewModel;
        return new ViewSimpleResponse($"Добро пожаловать. {(viewModel.IsRegistered?"/start":"/register")}");
    }
}