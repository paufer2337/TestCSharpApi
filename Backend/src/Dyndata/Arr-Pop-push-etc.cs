namespace Dyndata;
// Arr: Pop/push, shift/unshift and other basic methods

public partial class Arr
{
    public int Push(params object[] values)
    {
        Splice(Length, 0, values);
        return Length;
    }

    public object Pop()
    {
        return Splice(Length - 1, 1)[0];
    }

    public int Unshift(params object[] values)
    {
        Splice(0, 0, values);
        return Length;
    }

    public object Shift()
    {
        return Splice(0, 1)[0];
    }

    public int IndexOf(object value)
    {
        return memory.IndexOf(value);
    }

    public int LastIndexOf(object value)
    {
        return memory.LastIndexOf(value);
    }

    public bool Includes(object value)
    {
        return memory.Contains(value);
    }

    public Arr Reverse()
    {
        memory.Reverse();
        return this;
    }

    public Arr ToReversed()
    {
        return Slice().Reverse();
    }
}