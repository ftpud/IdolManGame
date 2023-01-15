using LiteDB;

namespace GameShared.Helpers;

public class DbHelper
{
    public static ILiteCollection<T> GetDbCollection<T>(String dbname, String collection) {
        var db = new LiteDatabase($"Filename={dbname};connection=shared");
        return db.GetCollection<T>(collection);
    }
}