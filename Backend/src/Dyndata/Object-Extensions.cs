namespace Dyndata;

public static class ObjectExtensions
{
    public static Obj Make(this object obj)
    {
        return new Obj(obj);
    }
}