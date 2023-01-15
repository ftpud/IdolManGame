using GameShared.Helpers;
using GameShared.Services.Entity;
using LiteDB;
using UglyAppFramework.DependencyManager.Attributes;

namespace GameShared.Services;

[Managed]
public class SongManager
{
    private ILiteCollection<SongEntity> _songs { get; set; }

    [PostConstruct]
    public void Init()
    {
        _songs = DbHelper.GetDbCollection<SongEntity>("userData.db", "songs");
    }

    public List<SongEntity> GetAllSongsByUid(long uid)
    {
        return _songs.Query().Where(q => q.tguid == uid).ToList();
    }

    public void InsertSong(SongEntity song)
    {
        _songs.Insert(song);
    }

    public SongEntity GetSongById(int id)
    {
        return _songs.FindById(id);
    }
}