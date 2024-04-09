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

    public Dynamic Slice(int start)
    {
        return Slice(start, Length());
    }

    public Dynamic Slice()
    {
        return Slice(0, Length());
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

    public Dynamic ToSpliced(int start, int deleteCount, params object[] values)
    {
        var copy = Slice();
        copy.Splice(start, deleteCount, values);
        return copy;
    }

    public Dynamic Reverse()
    {
        arrMemory.Reverse();
        return this;
    }

    public Dynamic ToReversed()
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
        return arrMemory.IndexOf(value);
    }

    public int LastIndexOf(object value)
    {
        return arrMemory.LastIndexOf(value);
    }

    public bool Includes(object value)
    {
        return arrMemory.Contains(value);
    }
}