using System.Collections;

public partial class Arr
{
    private static dynamic tryToObj(dynamic value)
    {
        var t = value.GetType();
        var isPrimitive =
            t.IsPrimitive ||
            t == typeof(Decimal) ||
            t == typeof(String);
        var isObjAlready = value is Obj;

        var isList = value as IEnumerable != null;

        if (!isPrimitive && !isList && !isObjAlready)
        {
            try { value = new Obj(value); }
            catch (Exception) { }
        }
        return value!;
    }

    private static Arr _(params dynamic[] obj)
    {
        var x = new Arr();
        foreach (var item in obj[0]) { x.Push(tryToObj(item)); }
        return x;
    }

    public Arr Map(Func<dynamic, dynamic> func)
    {
        return _(memory.Select(func));
    }

    public Arr Map(Func<dynamic, int, dynamic> func)
    {
        return _(memory.Select(func));
    }

    public Arr Filter(Func<dynamic, bool> func)
    {
        return _(memory.Where(func));
    }

    public Arr Filter(Func<dynamic, int, bool> func)
    {
        return _(memory.Where(func));
    }

    public dynamic Find(Func<dynamic, bool> func)
    {
        return memory.FirstOrDefault(func)!;
    }

    public dynamic Find(Func<dynamic, int, bool> func)
    {
        return memory.FirstOrDefault(func)!;
    }

    public dynamic FindLast(Func<dynamic, bool> func)
    {
        return memory.LastOrDefault(func)!;
    }

    public dynamic FindLast(Func<dynamic, int, bool> func)
    {
        return memory.LastOrDefault(func)!;
    }

    public int FindIndex(Func<dynamic, bool> func)
    {
        return IndexOf(memory.FirstOrDefault(func)!);
    }

    public int FindIndex(Func<dynamic, int, bool> func)
    {
        return IndexOf(memory.FirstOrDefault(func)!);
    }

    public int FindLastIndex(Func<dynamic, bool> func)
    {
        return LastIndexOf(memory.LastOrDefault(func)!);
    }

    public int FindLastIndex(Func<dynamic, int, bool> func)
    {
        return LastIndexOf(memory.LastOrDefault(func)!);
    }




}