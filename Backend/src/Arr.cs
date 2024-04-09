using System.Collections;

// Indexer + IEnumarable implementation
// so that Arr behaves like a enumerable

public partial class Arr : IEnumerable
{
    public object this[int index]
    {
        get
        {
            return index >= 0 && index < memory.Count() ?
                memory[index] : null!;
        }
        set
        {
            if (index < 0) { return; }
            while (index >= memory.Count()) { memory.Add(null!); }
            memory[index] = value;
        }
    }

    private List<object> memory = new List<object>();

    public Arr() { }

    public Arr(object[] objects)
    {
        foreach (var obj in objects)
        {
            memory.Add(obj);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public ArrEnum GetEnumerator()
    {
        return new ArrEnum(memory.ToArray());
    }
}

// When you implement IEnumerable, you must also implement IEnumerator.
public class ArrEnum : IEnumerator
{
    public object[] _arr;

    // Enumerators are positioned before the first element
    // until the first MoveNext() call.
    int position = -1;

    public ArrEnum(object[] list)
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

    object IEnumerator.Current
    {
        get
        {
            return Current!;
        }
    }

    public object Current
    {
        get
        {
            try
            {
                return _arr[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
}