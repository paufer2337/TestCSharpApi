namespace Dyndata;
using A = Arr;
using O = Obj;

// Static factory functions for Arr, Obj
// + Log

public static class Factory
{
    public static A Arr()
    {
        return new A();
    }

    public static A Arr(IEnumerable items)
    {
        return new A(items);
    }

    public static A Arr(params dynamic[] items)
    {
        if (items.Length == 1 && items[0] is Arr) { return (Arr)items[0]; }
        return new A(items);
    }

    public static dynamic Obj()
    {
        return new O();
    }

    public static dynamic Obj(object source)
    {
        return new O(source);
    }

    public static void Log(params object[] paras)
    {
        Console.WriteLine(JSON.StringifyForLog(paras) + "\r\n");
    }
}