namespace IdolManGame.Game.Repository.UserManagement.Entity;

public class UnitEntity
{
    public int _id { get; set; }
    public long OwnerId { get; set; }
    public string UnitName { get; set; }
    public List<int> UnitMembers { get; set; }
    
}