namespace Dyndata;

// Utils:
//
// TryToObjOrArr:
// Convert all IEnumerables to Arrs
// and all objects to Obj, if possible

public static class Utils
{
    public static dynamic TryToObjOrArr(dynamic value)
    {
        Type t = typeof(int);
        if (value != null) { t = value.GetType(); }
        var isPrimitive = value == null || t.IsPrimitive || t == typeof(String) || t == typeof(Decimal);
        var isList = !isPrimitive && value is IEnumerable;
        var isObject = !isPrimitive && !isList && value is object;

        if (isList && value is not Arr)
        {
            try { value = new Arr(value); }
            catch (Exception) { };
        }

        if (isObject && value is not Obj)
        {
            try { value = new Obj(value!); }
            catch (Exception) { };
        }

        return value!;
    }
}