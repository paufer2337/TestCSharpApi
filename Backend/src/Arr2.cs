public partial class Arr
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

    public List<object> ToList()
    {
        return memory.ToList();
    }

    public object[] ToArray()
    {
        return memory.ToArray();
    }

    public int Length()
    {
        return memory.Count();
    }

    public Arr Slice(int start, int end)
    {
        start = start < 0 ? Length() + start : start;
        start = start < 0 ? 0 : start;
        end = end < 0 ? Length() + end : end;
        end = end < 0 ? 0 : end;
        var collected = new List<object>();
        for (var i = start; i < end; i++)
        {
            collected.Add(memory[i]);
        }
        return new Arr(collected.ToArray());
    }

    public Arr Slice(int start)
    {
        return Slice(start, Length());
    }

    public Arr Slice()
    {
        return Slice(0, Length());
    }

    public Arr Splice(int start, int deleteCount, params object[] values)
    {
        start = start < 0 ? Length() + start : start;
        start = start < 0 ? 0 : start;
        var removed = new List<object>();
        for (var i = 0; i < deleteCount; i++)
        {
            removed.Add(memory[start]);
            memory.RemoveAt(start);
        }
        for (var i = values.Length - 1; i >= 0; i--)
        {
            memory.Insert(start, values[i]);
        }
        return new Arr(removed.ToArray());
    }

    public Arr ToSpliced(int start, int deleteCount, params object[] values)
    {
        var copy = Slice();
        copy.Splice(start, deleteCount, values);
        return copy;
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
}