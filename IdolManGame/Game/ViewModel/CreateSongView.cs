using GameShared.Services.Entity;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class CreateSongView : IView
{

    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (CreateSongPage)sharedViewModel;
        return new ViewSimpleResponse(@$"Создать песню
{viewModel.SongName}
{viewModel.SongData}
",
            ViewHelper.ButtonBuilder.Create().AddBackButton("Назад").Add("Создать", "/create", viewModel.SongName.Text != "").Build());
    }

    private String Convert(SongEntity song)
    {
        return song.SongName;
    }
}