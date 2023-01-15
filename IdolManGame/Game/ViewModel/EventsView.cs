using IdolManGame.Game.Repository.UserManagement;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class EventsView : IView
{
    [Inject] private UserManager _userManager { get; set; }

    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (EventsPage)sharedViewModel;
        return new ViewSimpleResponse(@$"Выбирите тип эвента: 
{viewModel.cheapEventButton}
",
            ViewHelper.ButtonBuilder.BuildBackButton("Назад"));
    }
}