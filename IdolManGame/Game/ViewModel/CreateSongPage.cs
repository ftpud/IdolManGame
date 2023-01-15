using System.Text;
using GameShared.Services;
using GameShared.Services.Entity;
using GameShared.UI.FormElements;
using Telegram.Bot.Types;
using UglyAppFramework.DependencyManager.Attributes;
using UglyTgApplication.Attributes;

namespace IdolManGame.Game.ViewModel;

[BindView(typeof(CreateSongView))]
public class CreateSongPage : UglyTgApplication.States.ViewModel
{
    [Inject] private SongManager _songManager { get; set; }
    
    internal List<SongEntity> MySongs => _songManager.GetAllSongsByUid(Context.UserId);
    
    internal FormTextBox SongName { get; set; }
    internal FormTextBox SongData { get; set; }

    [PostConstruct]
    public void Init()
    {
        SongName = new FormTextBox(this, "Название песни");
        SongData = new FormTextBox(this, "????");
        SongData.Text = GenerateSongData();
    }

    [Callback(Trigger = "/back")]
    public void BackCallback(Update update)
    {
        Pop();
    }
    
    [Callback(Trigger = "/create")]
    public void CreateCallback(Update update)
    {
        _songManager.InsertSong(new SongEntity()
        {
            tguid = Context.UserId,
            SongName = SongName.Text,
            SongData = SongData.Text
        });
        Pop();
    }

    private Random rand = new Random(45644);
    private String GenerateSongData()
    {
        int dataElements = 20;
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < dataElements; i++)
        {
            builder.Append((char)rand.Next(65, 120));
        }

        return builder.ToString();
    }
}