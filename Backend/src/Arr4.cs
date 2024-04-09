using System.Linq.Expressions;

partial class Arr
{
    // Aliases with classic C#/Linq naming
    public Func<object, bool> Contains => Includes;
    public Func<Expression, Dynamic, dynamic> Select => Map;
    public Func<Expression, Dynamic, dynamic> Where => Filter;
    public Func<Expression, Dynamic, dynamic> First => Find;
    public Func<Expression, Dynamic, dynamic> Last => FindLast;
    public Func<Expression, Dynamic, bool> Any => Some;
    public Func<Expression, Dynamic, bool> All => Every;

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
}