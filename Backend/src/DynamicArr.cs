using Z.Expressions;
using AgileObjects.ReadableExpressions;
using System.Linq.Expressions;

public partial class Dynamic
{

    private object TryDynamic(object value)
    {
        try
        {
            return new Dynamic(value);
        }
        catch (Exception)
        {
            return value;
        }
    }

    public int Length()
    {
        return arrMemory.Count();
    }

    public Dynamic Splice(int start, int deleteCount, params object[] values)
    {
        start = start < 0 ? Length() + start : start;
        start = start < 0 ? 0 : start;
        var removed = new List<object>();
        for (var i = 0; i < deleteCount; i++)
        {
            removed.Add(arrMemory[start]);
            arrMemory.RemoveAt(start);
        }
        for (var i = values.Length - 1; i >= 0; i--)
        {
            arrMemory.Insert(start, values[i]);
        }
        return new Dynamic(removed);
    }

    public Dynamic Slice(int start, int end)
    {
        start = start < 0 ? Length() + start : start;
        start = start < 0 ? 0 : start;
        end = end < 0 ? Length() + end : end;
        end = end < 0 ? 0 : end;
        var collected = new List<object>();
        for (var i = start; i < end; i++)
        {
            collected.Add(arrMemory[i]);
        }
        return new Dynamic(collected);
    }

    public Dynamic Reverse()
    {
        arrMemory.Reverse();
        return this;
    }

    public Dynamic Slice(int start)
    {
        return Slice(start, Length());
    }

    public Dynamic Slice()
    {
        return Slice(0, Length());
    }

    public int Push(params object[] values)
    {
        Splice(Length(), 0, values);
        return Length();
    }

    public object Pop()
    {
        return TryDynamic(Splice(Length() - 1, 1)[0]);
    }

    public int Unshift(params object[] values)
    {
        Splice(0, 0, values);
        return Length();
    }

    public object Shift()
    {
        return TryDynamic(Splice(0, 1)[0]);
    }

    public static Dynamic Prepare(Expression lambda, object dependencies = null!)
    {
        dynamic result = new Dynamic();
        result.lambda = lambda.ToReadableString();
        result.dependencies = dependencies != null ? dependencies : new { };
        return result;
    }

    private Dynamic LinqRun(string linqExpression, dynamic prepared)
    {
        dynamic deps = new Dynamic(prepared.dependencies);
        deps.__x_ = this.ToArray();
        return new Dynamic(Eval.Execute(
            "__x_." + linqExpression,
            deps.ToDictionary()
        ));
    }

    public Dynamic Map(dynamic prepared)
    {
        return LinqRun($"Select({prepared.lambda});", prepared);
    }

    public Dynamic Filter(dynamic prepared)
    {
        return LinqRun($"Where({prepared.lambda});", prepared);
    }

    /*public Dynamic Do(FuncWrapper f)
    {
        var arr = this.ToArray();
        return new Dynamic(
            f.Action == "map" ? arr.Select(f.FuncObjObj) :
            f.Action == "mapI" ? arr.Select(f.FuncObjIntObj) :
            f.Action == "filter" ? arr.Where(f.FuncObjBool) :
            new { }
        );
    }*/
}