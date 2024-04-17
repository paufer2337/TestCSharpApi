namespace Dyndata;
using A = Arr;
using O = Obj;

// Static factory functions for Arr and Obj

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
        return new A(items);
    }

    public static O Obj()
    {
        return new O();
    }

    public static O Obj(object source)
    {
        return new O(source);
    }
}