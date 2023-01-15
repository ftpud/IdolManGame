using WorldEngine.Game.Entity;

namespace EventEngine;

public class RateCalculator
{
    public static float CalcAgeLikeness(int expected, int received)
    {
        float multiplier = 0.3f;
        float diff = Math.Abs(expected - received);
        return Math.Max(1f - (diff * multiplier), 0);
    }

    public static float CalcHeightLikeness(int expected, int received)
    {
        float multiplier = 0.1f;
        float diff = Math.Abs(expected - received);
        return Math.Max(1f - (diff * multiplier), 0);
    }

    public static float CalcWeightLikeness(int expected, int received)
    {
        float multiplier = 0.05f;
        float diff = Math.Abs(expected - received) * multiplier;
        return Math.Max(1f - diff, 0);
    }

    public static float CalcSkillLikeness(int expected, int received, float skillRequirementRate)
    {
        float multiplier = 0.04f;
        float diff = ((((float)expected) * skillRequirementRate) - received) * multiplier;
        return Math.Min(Math.Max(1f - diff, 0), 1);
    }

    public static float CalcAppearanceLikeness(string expectedString, string receivedString)
    {
        float output = 1;

        for (int i = 0; i < expectedString.Length; i++)
        {
            int expected = (int)expectedString[i];
            int received = (int)receivedString[i];

            float multiplier = 0.025f;
            float diff = Math.Abs(expected - received) * multiplier;
            output *= Math.Max(1f - (diff * diff), 0);
        }

        return output;
    }

    public static float CalcSexLikeness(int expected, int received)
    {
        return 1 - Math.Abs(expected - received);
    }

    public static float Average(params float[] values)
    {
        return values.Sum() / values.Length;
    }

    public static Random Randomizer = new Random(22222);

    public static void UpdateViewerStats(HumanEntity viewer, long groupId, int idolId, float likeness)
    {
        if (!viewer.GroupPreference.ContainsKey(groupId))
        {
            viewer.GroupPreference.Add(groupId, likeness);
        }
        else
        {
            viewer.GroupPreference[groupId] = Average(viewer.GroupPreference[groupId], likeness);
        }

        // Узнаваемость/Запоминаемость
        if (Randomizer.Next(0, 100) < likeness * 100)
        {
            if (viewer.Recognition.ContainsKey(idolId))
            {
                viewer.Recognition[idolId] = Average(viewer.Recognition[idolId] * 1.5f, viewer.Recognition[idolId]);
            }
            else
            {
                viewer.Recognition[idolId] = likeness;
            }
        }

        // Симпатия
        if (viewer.Like.ContainsKey(idolId))
        {
            viewer.Like[idolId] = Average(viewer.Like[idolId], likeness);
        }
        else
        {
            viewer.Like[idolId] = likeness;
        }

        // Oshi
        if (viewer.Like[idolId] >= 0.8f)
        {
            viewer.Oshimen = viewer.Like.OrderByDescending(v => v.Value).First().Key;
        }

        // Taste improvement
        // TODO
    }
}