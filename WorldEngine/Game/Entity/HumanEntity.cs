namespace WorldEngine.Game.Entity;

public class HumanEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Energy { get; set; }
    public int EnergyMax { get; set; }
    public HumanProperties OwnProperties { get; set; }
    public HumanProperties PreferredProperties { get; set; }
    
    public Dictionary<long, float> GroupPreference { get; set; } = new Dictionary<long, float>();
    public Dictionary<int, float> Recognition { get; set; } = new Dictionary<int, float>();
    public Dictionary<int, float> Like { get; set; } = new Dictionary<int, float>();

    public int Oshimen { get; set; }
    

    public long IsHiredBy { get; set; }
}