using GameShared.Services;
using GameShared.Services.Entity;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(RepertoireView))]
public class RepertoirePage : UglyTgApplication.States.ViewModel
{
    [Inject] private SongManager _songManager { get; set; }
    
    internal List<SongEntity> MySongs => _songManager.GetAllSongsByUid(Context.UserId);

    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }
    
    [Callback(Trigger = "/newSong")]
    public void NewSongCallback(Update update)
    {
        Push(new CreateSongPage());
    }
}