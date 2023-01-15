using System.Text;
using WorldEngine.Game.Entity;

namespace WorldEngine.Game.Repository;

public class HumanGenerator
{
    public List<HumanEntity> CreateHumanEntities(int number = 100_000)
    {
        List<HumanEntity> humanEntities = new List<HumanEntity>();
        NameGenerator nameGenerator = new NameGenerator();

        for (int i = 0; i < number; i++)
        {
            HumanProperties ownProperties = GenerateRandomProperties(2);
            HumanProperties preferredProperties = GenerateRandomProperties();

            HumanEntity entity = new HumanEntity()
            {
                Id = i + 1,
                Name = nameGenerator.GenerateName(ownProperties.Sex),
                Energy = 100,
                EnergyMax = 100,
                OwnProperties = ownProperties,
                PreferredProperties = preferredProperties
            };

            humanEntities.Add(entity);
        }

        return humanEntities;
    }

    private Random rand = new Random(12345);

    private HumanProperties GenerateRandomProperties(int skillDiv = 1)
    {
        HumanProperties props = new HumanProperties();

        props.Age = rand.Next(15, 60);
        props.Sex = rand.Next(0, 2);
        props.Height = rand.Next(140, 180);
        props.Weight = rand.Next(45, 70);
        props.ApperanceData = GenerateAppearanceData();
        props.Dancing = rand.Next(0, 100 / skillDiv);
        props.Singing = rand.Next(0, 100 / skillDiv);
        props.Acting = rand.Next(0, 100 / skillDiv);
        props.VoiceActing = rand.Next(0, 100 / skillDiv);

        return props;
    }

    private String GenerateAppearanceData()
    {
        int appearanceElementNumber = 20;
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < appearanceElementNumber; i++)
        {
            builder.Append((char)rand.Next(65, 120));
        }

        return builder.ToString();
    }
    
}

public class NameGenerator
{
    private Random r = new Random(873246);
    private String[] _maleNames;
    private String[] _femaleNames;
    private String[] _familyNames;

    public NameGenerator()
    {
        _maleNames = File.ReadAllLines("Resources/MaleNames.txt");
        _femaleNames = File.ReadAllLines("Resources/FemaleNames.txt");
        _familyNames = File.ReadAllLines("Resources/NeutralNames.txt");
    }
    
    public string GenerateName(int sex)
    {
        string name = "";
        if (sex == 1)
        {
            name = _femaleNames[r.Next(0, _femaleNames.Length)];
        }
        else
        {
            name = _maleNames[r.Next(0, _maleNames.Length)];
        }

        name += " " + _familyNames[r.Next(0, _familyNames.Length)];
        return name;
    }
}