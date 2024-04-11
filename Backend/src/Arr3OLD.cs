/*using Z.Expressions;
using AgileObjects.ReadableExpressions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

public partial class Arr
{
    public static Dynamic Prepare(Expression lambda, object dependencies = null!)
    {
        dynamic result = new Dynamic();
        var lam = lambda.ToReadableString();
        // Fix errors in conversion done by ToReadableString
        lam = lam.Replace("−", "-");
        lam = Regex.Replace(lam, @"(\d{1,}),*(\d*)d", "$1__temp__$2");
        lam = Regex.Replace(lam, @"__temp__(\d)", ".$1").Replace("__temp__", ".0");
        // Build result
        result.lambda = lam;
        result.dependencies = new Dynamic(dependencies != null ? dependencies : new { });
        return result;
    }

    private dynamic LinqRun(string linqExpression, dynamic prepared)
    {
        dynamic deps = prepared.dependencies;
        deps.__x_ = !deps.HasKey("__reverse_") ? memory : ToReversed().ToList();
        var result = Eval.Execute(
            "__x_." + linqExpression,
            deps.ToDictionary()
        );
        if (result == null) { return null!; }
        var t = result.GetType();
        return t.IsPrimitive || t == typeof(Decimal) || t == typeof(String)
            ? result : new Dynamic(result);
    }

    public Arr Map(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return new Arr(LinqRun($"Select({prepared.lambda});", prepared).ToArray());
    }

    public Arr Filter(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return new Arr(LinqRun($"Where({prepared.lambda});", prepared).ToArray());
    }

    public dynamic Find(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return LinqRun($"FirstOrDefault({prepared.lambda},null);", prepared);
    }

    public dynamic FindLast(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return LinqRun($"LastOrDefault({prepared.lambda},null);", prepared);
    }

    public int FindIndex(Expression lambda, object deps = null!)
    {
        return memory.IndexOf(Find(lambda, deps));
    }

    public int FindLastIndex(Expression lambda, object deps = null!)
    {
        return memory.LastIndexOf(FindLast(lambda, deps));
    }

    public bool Some(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return LinqRun($"Any({prepared.lambda});", prepared);
    }

    public bool Every(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return LinqRun($"All({prepared.lambda});", prepared);
    }

    public dynamic Reduce(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return LinqRun($"Aggregate({prepared.lambda});", prepared);
    }

    public dynamic Reduce(Expression lambda, dynamic initVal, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        prepared.dependencies.__seed = initVal;
        return LinqRun($"Aggregate(__seed,{prepared.lambda});", prepared);
    }

    public dynamic ReduceRight(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        prepared.dependencies.__reverse_ = true;
        return Reduce(prepared);
    }

    public dynamic ReduceRight(Expression lambda, dynamic initVal, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        prepared.dependencies.__reverse_ = true;
        return Reduce(prepared, initVal);
    }

    public Arr Sort()
    {
        memory.Sort();
        return this;
    }

    public Arr Sort(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        // Linq Sort can only handle integer return values
        // but accepting any numeric value is more flexible so:
        // 0 -> 0, >0 -> 1, <0 -> -1
        var p = prepared.lambda.Split("=> ", 2);
        var x = @$"
            {p[0]}=> {{
                var _a_b = {p[1]};
                return _a_b == 0 ? 0 : _a_b > 0 ? 1 : -1;
            }}";
        LinqRun($"Sort({x});", prepared);
        return this;
    }

    public Arr ToSorted()
    {
        return Slice().Sort();
    }

    public Arr ToSorted(Expression lambda, object deps = null!)
    {
        dynamic prepared = Prepare(lambda, deps);
        return Slice().Sort(prepared);
    }
}*/