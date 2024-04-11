using System.Collections;

// Indexer + IEnumarable implementation
// so that Arr behaves like a enumerable

public partial class Arr : IEnumerable<object>
{
    private List<object> memory = [];

    public Arr() { }

    public Arr(params dynamic[] items)
    {
        foreach (var item in items) { Push(item); }
    }

    public dynamic this[int index]
    {
        get
        {
            return index >= 0 && index < memory.Count() ?
                memory[index] : null!;
        }
        set
        {
            if (index < 0) { return; }
            while (index >= memory.Count()) { Push(null!); }
            memory[index] = value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<object> GetEnumerator()
    {
        return new ArrEnum(memory.ToArray());
    }
}

// When you implement IEnumerable, you must also implement IEnumerator.
public class ArrEnum : IEnumerator<object>
{
    public dynamic[] _arr;

    public void Dispose() { _arr = null!; }

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    int position = -1;

    public ArrEnum(dynamic[] list)
    {
        _arr = list;
    }

    public bool MoveNext()
    {
        position++;
        return position < _arr.Length;
    }

    public void Reset()
    {
        position = -1;
    }

    dynamic IEnumerator.Current
    {
        get
        {
            return Current!;
        }
    }

    public dynamic Current
    {
        get
        {
            try
            {
                return (dynamic)(_arr[position]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
}