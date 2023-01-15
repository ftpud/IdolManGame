using IdolManGame.Game.Repository.UserManagement;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class AuditionRequestView : IView
{
    [Inject] private UserManager _userManager { get; set; }

    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (AuditionRequestPage)sharedViewModel;
        return new ViewSimpleResponse($@"
Экран настройки прослушивания.
Пока здесь пусто.

Посмотреть присланные анкеты /forms",
            ViewHelper.ButtonBuilder.Create()
                .Add("Назад", "/back")
                .Add("Запустить", "/start")
                .Build()
        );
    }
}