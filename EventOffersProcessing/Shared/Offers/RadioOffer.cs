using EventEngine;
using EventOffersProcessing.Shared.Entity;
using EventOffersProcessing.Shared.Offers.RadioOfferPages;
using GameShared.Services;
using Newtonsoft.Json;
using UglyAppFramework.DependencyManager.Attributes;
using WorldEngine.Game.Entity;

namespace EventOffersProcessing.Shared.Offers;

[Managed(Identifier = "RadioOfferBean")]
public class RadioOffer : OfferBase
{
    [Inject] private OfferManager _offerManager { get; set; }
    [Inject] private WorldEngine.Game.Engine.WorldEngine _worldEngine { get; set; }

    [Inject] private SongManager _songManager { get; set; }

    public override UglyTgApplication.States.ViewModel GetViewModel(OfferEntity e)
    {
        return new RadioOfferPage(e);
    }

    public override void Process(OfferEntity e)
    {
        

        Console.WriteLine("Getting auditory");
        var report = DoEvent(GetAuditory(), e);
        Console.WriteLine("Done");
        
        e.state = OfferState.Completed;
        e.Report = report;
        _offerManager.PushOffer(e);
    }


    private Random rnd = new Random((int)(new DateTimeOffset(DateTime.Now.ToUniversalTime()).ToUnixTimeMilliseconds()));

    public int[] GetAuditory()
    {
        // 2%-4%
        float auditoryRate = ((float)rnd.Next(200,400)) / 10000;
        int max = _worldEngine.WorldSize;
        int n = (int)(max * auditoryRate);
        return Enumerable
            .Range(0, int.MaxValue)
            .Select(_ => rnd.Next(1, max))
            .Distinct()
            .Take(n-1)
            .ToArray();
    }

    string DoEvent(int[] auditory, OfferEntity e)
    {
        var offerData = GetRadioOfferEntity(e);
        var singer = _worldEngine.GetHumanById(offerData.SingerId);
        var song = _songManager.GetSongById(offerData.SongId);

        Console.WriteLine($"{singer.Name} - {song.SongName}");

        int reportLikes = 0;
        
        foreach (var humanId in auditory)
        {
            var viewer = _worldEngine.GetHumanById(humanId);

            float result = RateCalculator.Average(
                RateCalculator.CalcSkillLikeness(viewer.PreferredProperties.Singing,
                    singer.OwnProperties.Singing, 1f),
                RateCalculator.CalcSkillLikeness(viewer.PreferredProperties.VoiceActing,
                    singer.OwnProperties.VoiceActing, 0.1f),
                RateCalculator.CalcAppearanceLikeness(
                    viewer.PreferredProperties.ApperanceData, song.SongData),
                RateCalculator.CalcSexLikeness(viewer.PreferredProperties.Sex, singer.OwnProperties.Sex)
            );

            RateCalculator.UpdateViewerStats(viewer, singer.IsHiredBy, singer.Id, result);

            if (viewer.Recognition.ContainsKey(singer.Id) && viewer.Recognition[singer.Id] > 0.75f)
            {
                reportLikes++;
            }

            _worldEngine.BulkUpdate(viewer);
        }
        _worldEngine.BulkFinalize();
        
        Console.WriteLine("Generating report");
        Console.WriteLine(GenerateReport(singer.Id));
        
        return $@"
Завершено.
По результатам исследования аудитория составила примерно {auditory.Length} человек.
{reportLikes} человек отметили, что композиция им понравилась.";
        
    }


    public RadioOfferEntity GetRadioOfferEntity(OfferEntity entity)
    {
        return JsonConvert.DeserializeObject<RadioOfferEntity>(entity.OfferData);
    }
    
    
    
    private string GenerateReport(int entityId)
    {
        int recognition = 0;
        int recognitionOver66 = 0;

        int totalDislike = 0;
        int totalNeutral = 0;
        int totalLike = 0;
        int totalOshi = 0;


        // _worldEngine.WorldCollection.FindAll()
        foreach (HumanEntity entity in _worldEngine.WorldCollection.FindAll())
        {
            if (entity.Recognition.ContainsKey(entityId) && entity.Recognition[entityId] > 0)
            {
                recognition++;
            }
            if (entity.Recognition.ContainsKey(entityId) && entity.Recognition[entityId] > 0.66f)
            {
                recognitionOver66++;
            }
            if (entity.Like.ContainsKey(entityId) && entity.Like[entityId] > 0.66f)
            {
                totalLike++;
            }
            if (entity.Like.ContainsKey(entityId) && entity.Like[entityId] <= 0.33f)
            {
                totalDislike++;
                
            }
            if (entity.Like.ContainsKey(entityId) && entity.Like[entityId] > 0.33f && entity.Like[entityId] <= 0.66f)
            {
                totalNeutral++;
            }
            if (entity.Oshimen == entityId)
            {
                totalOshi++;
            }
        }

        int multiplier = 1;


        return $@"
<b>Узнают</b>: {multiplier * recognition}
<b>Запомнили</b>: {multiplier * recognitionOver66}

<b>Не нравится</b>: {multiplier * totalDislike}
<b>Нейтрально</b>: {multiplier * totalNeutral}
<b>Нравится</b>: {multiplier * totalLike}

<b>Оши</b>: {multiplier * totalOshi}
";
    }
}