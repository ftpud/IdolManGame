using GameShared.UI.FormElements;
using IdolManGame.Game.Repository.UserManagement;
using IdolManGame.Game.Repository.UserManagement.Entity;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(RegistrationView))]
public class RegistrationPage : UglyTgApplication.States.ViewModel
{
    [Inject] private UserManager _userManager { get; set; }

    public bool IsFormReady => nameTextBox.Text != "" && groupTextBox.Text != "";
    public FormTextBox nameTextBox { get; set; }
    public FormTextBox groupTextBox { get; set; }

    public override void Initialize()
    {
        nameTextBox = new FormTextBox(this, "Введите ваше имя:");
        groupTextBox = new FormTextBox(this, "Введите название группы:");
        base.Initialize();
    }
    
    [Callback(Trigger = "/reg")]
    public void RegisterCallback(Update update)
    {
        _userManager.Register(new UserEntity()
        {
            _id = Context.CurrentUserId.Identifier.Value,
            Cash = 10_000,
            ChatId = Context.CurrentUserId.Identifier.Value,
            GroupName = groupTextBox.Text,
            Nickname = nameTextBox.Text
        });
        
        Pop();
    }
}