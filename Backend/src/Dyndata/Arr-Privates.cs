namespace Dyndata;
using System.Collections;

// Arr: All private members

public partial class Arr
{
    private List<object> memory = [];

    private static dynamic TryToObj(dynamic value)
    {
        var t = value.GetType();
        var isPrimitive =
            t.IsPrimitive ||
            t == typeof(Decimal) ||
            t == typeof(String);
        var isObjAlready = value is Obj;
        var isArrAlready = value is Arr;

        var isList = value as IEnumerable != null;

        if (!isPrimitive && !isList && !isObjAlready)
        {
            try { value = new Obj(value); }
            catch (Exception) { }
        }
        else if (!isPrimitive && isList && !isArrAlready)
        {
            try { value = new Arr(value.ToArray()); }
            catch (Exception) { }
        }
        return value!;
    }

    private object[] Rrh() // rrh = Reduce Right Helper
    {
        return ToReversed().ToArray();
    }
}