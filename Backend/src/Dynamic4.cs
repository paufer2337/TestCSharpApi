using System.Linq.Expressions;

partial class Dynamic
{
    // Aliases with classic C#/Linq naming
    public Func<object, bool> Contains => Includes;
    public Func<dynamic, dynamic> Select => Map;
    public Func<dynamic, dynamic> Where => Filter;
    public Func<dynamic, dynamic> First => Find;
    public Func<dynamic, dynamic> Last => FindLast;
    public Func<dynamic, bool> Any => Some;
    public Func<dynamic, bool> All => Every;

    public dynamic Aggregate(dynamic func)
    {
        return Reduce(func);
    }

    public dynamic Aggregate(dynamic seed, dynamic func)
    {
        // Not really an alias 
        // - since Aggregate uses a reversed parameter order
        return Reduce(func, seed);
    }

    // AggregateRight does not exist in Linq, but add it anyway
    public dynamic AggregateRight(dynamic func)
    {
        return ReduceRight(func);
    }

    public dynamic AggregateRight(dynamic seed, dynamic func)
    {
        // Not really an alias 
        // - since Aggregate uses a reversed parameter order
        return ReduceRight(func, seed);
    }

    // Experiment
    public Holder _()
    {
        return new Holder(this);
    }
}

public class Holder
{
    private Dynamic? dRef;

    public Holder(Dynamic dReff)
    {
        dRef = dReff;
    }

    public dynamic Map(Expression lambda, object dependencies = null!)
    {
        var prepared = Dynamic.Prepare(lambda, dependencies);
        return dRef!.Map(prepared);
    }
}