namespace EventEngine;

[System.AttributeUsage(System.AttributeTargets.Class)]
public class IdolEventAttribute : System.Attribute
{
    public string dbname;
    public string collectionName;
    public int delay = 1000;

    public IdolEventAttribute()
    {
        
    }

}