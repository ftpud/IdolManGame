using AdvertisementEvent.db;
using EventEngine;
using WorldEngine.Game.Entity;

namespace AdvertisementEvent.AdTypes;

public class CheapAdsPerformance : AdPerformanceBase
{
    // Раздаём листовки

    public override List<HumanEntity> GetAuditory(AdEventRequest request)
    {
        
        return _worldEngine.WorldCollection.Find($"RANDOM(0,{_worldEngine.WorldSize}) < 1000").ToList();
    }

    public override void Perform(List<HumanEntity> auditory, AdEventRequest request)
    {
        var toBeUpdated = auditory;
        HumanEntity participant = _worldEngine.GetHumanById(request.charactedId);
        
        int i = 0;
        foreach (var viewer in toBeUpdated)
        {
            i++;
            float result = RateCalculator.Average(
                RateCalculator.CalcAgeLikeness(viewer.PreferredProperties.Age, participant.OwnProperties.Age),
                RateCalculator.CalcHeightLikeness(viewer.PreferredProperties.Height,
                    participant.OwnProperties.Height),
                RateCalculator.CalcWeightLikeness(viewer.PreferredProperties.Weight,
                    participant.OwnProperties.Weight),
                RateCalculator.CalcSkillLikeness(viewer.PreferredProperties.Acting,
                    participant.OwnProperties.Acting, 1f),
                RateCalculator.CalcSkillLikeness(viewer.PreferredProperties.VoiceActing,
                    participant.OwnProperties.VoiceActing, 1f),
                RateCalculator.CalcAppearanceLikeness(viewer.PreferredProperties.ApperanceData,
                    participant.OwnProperties.ApperanceData),
                RateCalculator.CalcSexLikeness(viewer.PreferredProperties.Sex, participant.OwnProperties.Sex)
            );

            RateCalculator.UpdateViewerStats(viewer, participant.IsHiredBy, participant.Id, result);
        }

        // updating
        Console.WriteLine($"Обновлено: " + i);
        _worldEngine.WorldCollection.Update(toBeUpdated);

        // var report = GenerateReport(participant.Id);
        //  _NotificationMessagesManager.PlaceMessage(participant.IsHiredBy,
        //     $"Рекламная кампания для [{participant.Name}] окончена." + report);
        // 
        Console.WriteLine($"Done");
        toBeUpdated.Clear();
    }

    public CheapAdsPerformance(WorldEngine.Game.Engine.WorldEngine engine) : base(engine)
    {
    }
}