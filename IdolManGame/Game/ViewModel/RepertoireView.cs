using GameShared.Services.Entity;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace IdolManGame.Game.ViewModel;

[Managed]
public class RepertoireView : IView
{

    public ViewResponse Display(IState sharedViewModel)
    {
        var viewModel = (RepertoirePage)sharedViewModel;
        return new ViewSimpleResponse(@$"
Управление репертуаром:
Композиции: (Создать /newSong)
{String.Join("\n", viewModel.MySongs.Select(s => Convert(s)))}

Синглы: (Создать)
1. .. (Отчет?)
2. ..

Видео клипы: (Создать)
1.


",
            ViewHelper.ButtonBuilder.BuildBackButton("Назад"));
    }

    private String Convert(SongEntity song)
    {
        return "- <code>" + song.SongName + "</code>";
    }
}