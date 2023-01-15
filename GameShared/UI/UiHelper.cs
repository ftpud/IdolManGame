

using WorldEngine.Game.Entity;

namespace GameShared.UI;

public class UiHelper
{
    public static String HumanEntityToIdolCardConverter(HumanEntity idol)
    {
        var sex = idol.OwnProperties.Sex == 0 ? "М" : "Ж";
        var sexEmo = idol.OwnProperties.Sex == 0 ? "♂" : "♀";
        var cardText = $@"<b>{idol.Name}</b> {sexEmo}
<code>Возраст:</code> {idol.OwnProperties.Age}
<code>Вес:</code> {idol.OwnProperties.Weight}
<code>Рост:</code> {idol.OwnProperties.Height}
<code>Пол:</code> {sex}

<code>Актёрское мастерство:</code> {idol.OwnProperties.Acting}
<code>Танцевальное мастерство:</code> {idol.OwnProperties.Dancing}
<code>Музыкальное мастерство:</code> {idol.OwnProperties.Singing}
<code>Развлекательно мастерство:</code> {idol.OwnProperties.VoiceActing}
";

        return cardText;
    }

    public static String HumanEntityToShortInfoConverter(HumanEntity idol)
    {
        var sexEmo = idol.OwnProperties.Sex == 0 ? "♂" : "♀";
        return $@"<b>{idol.Name}</b> {sexEmo}";
    }
}