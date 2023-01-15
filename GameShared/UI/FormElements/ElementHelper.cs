namespace GameShared.UI.FormElements;

public class ElementHelper
{
    private static int _uid = 0;
    public static int uid => ++_uid;
}