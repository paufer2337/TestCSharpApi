using Z.Expressions;
using AgileObjects.ReadableExpressions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

public partial class Dynamic
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
        deps.__x_ = !deps.HasKey("__reverse_") ? arrMemory : ToReversed().ToList();
        var result = Eval.Execute(
            "__x_." + linqExpression,
            deps.ToDictionary()
        );
        if (result == null) { return null!; }
        var t = result.GetType();
        return t.IsPrimitive || t == typeof(Decimal) || t == typeof(String)
            ? result : new Dynamic(result);

    }

    public dynamic Map(dynamic prepared)
    {
        return LinqRun($"Select({prepared.lambda});", prepared);
    }

    public dynamic Filter(dynamic prepared)
    {
        return LinqRun($"Where({prepared.lambda});", prepared);
    }

    public dynamic Find(dynamic prepared)
    {
        return LinqRun($"FirstOrDefault({prepared.lambda},null);", prepared);
    }

    public dynamic FindLast(dynamic prepared)
    {
        return LinqRun($"LastOrDefault({prepared.lambda},null);", prepared);
    }

    public int FindIndex(dynamic prepared)
    {
        return arrMemory.IndexOf(Find(prepared));
    }

    public int FindLastIndex(dynamic prepared)
    {
        return arrMemory.LastIndexOf(Find(prepared));
    }

    public bool Some(dynamic prepared)
    {
        return LinqRun($"Any({prepared.lambda});", prepared);
    }

    public bool Every(dynamic prepared)
    {
        return LinqRun($"All({prepared.lambda});", prepared);
    }

    public dynamic Reduce(dynamic prepared)
    {
        return LinqRun($"Aggregate({prepared.lambda});", prepared);
    }

    public dynamic Reduce(dynamic prepared, dynamic initVal)
    {
        prepared.dependencies.__seed = initVal;
        return LinqRun($"Aggregate(__seed,{prepared.lambda});", prepared);
    }

    public dynamic ReduceRight(dynamic prepared)
    {
        prepared.dependencies.__reverse_ = true;
        return Reduce(prepared);
    }

    public dynamic ReduceRight(dynamic prepared, dynamic initVal)
    {
        prepared.dependencies.__reverse_ = true;
        return Reduce(prepared, initVal);
    }

    public dynamic Sort(dynamic prepared)
    {
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

    public dynamic ToSorted(dynamic prepared)
    {
        return Slice().Sort(prepared);
    }
}