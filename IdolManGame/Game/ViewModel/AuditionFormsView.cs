using IdolManGame.Game.Repository.UserManagement;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class AuditionFormsView : IView
{
    [Inject] private UserManager _userManager { get; set; }

    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (AuditionFormsPage)sharedViewModel;
        return new ViewSimpleResponse($@"
Анкеты которые вам прислали:

{String.Join("\n", viewModel.auditionResponses)}

", ViewHelper.ButtonBuilder.BuildBackButton()
        );
    }
}