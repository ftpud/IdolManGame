using IdolManGame.Game.Repository.UserManagement;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(WelcomeView))]
public class WelcomePage : UglyTgApplication.States.ViewModel
{
    [Inject] private UserManager _userManager { get; set; }

    [Inject] private WorldEngine.Game.Engine.WorldEngine _worldEngine { get; set; }
    
    public bool IsRegistered => _userManager.isRegistered(Context.CurrentUserId.Identifier.Value);

    [Callback(Trigger = "/register")]
    public void RegisterCallback(Update update)
    {
        Push(new RegistrationPage());
    }
    
    [Callback(Trigger = "/start")]
    public void StartCallback(Update update)
    {
        if (IsRegistered)
        {
            Push(new MainPage());
        }
    }
    
}