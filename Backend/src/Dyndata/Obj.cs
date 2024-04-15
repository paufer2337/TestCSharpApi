namespace Dyndata;
using System.Dynamic;

public class Obj : DynamicObject
{

    private Dictionary<string, object> memory = new Dictionary<string, object>();

    public Obj() { }

    public Obj(object source)
    {
        Merge(source);
    }

    public void Merge(params object[] sources)
    {
        foreach (dynamic source in sources)
        {
            var keys = new Arr();
            var values = new Arr();
            if (source is Obj)
            {
                foreach (var key in source.GetKeys())
                {
                    keys.Push(key);
                    values.Push(source[key]);
                }
            }
            else
            {
                foreach (var prop in source.GetType().GetProperties())
                {
                    keys.Push(prop.Name);
                    values.Push(prop.GetValue(source));
                }
            }

            foreach (string key in keys)
            {
                if (key.StartsWith("SPREAD"))
                {
                    Merge(values.Shift());
                }
                else
                {
                    memory[key] = values.Shift();
                }
            }
        }
    }

    public object this[string key]
    {
        get { return memory[key]; }
        set { memory[key] = value; }
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        var key = binder.Name;
        return memory.TryGetValue(key, out result!);
    }

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        var key = binder.Name;
        memory[key] = value!;
        return true;
    }

    public bool Delete(string key)
    {
        memory.Remove(key);
        return true;
    }

    public bool HasKey(string key)
    {
        return memory.ContainsKey(key);
    }

    public Arr GetKeys()
    {
        return new Arr(memory.Keys.ToArray());
    }

    public Arr GetValues()
    {
        return new Arr(memory.Values.ToArray());
    }

    public Arr GetEntries()
    {
        var arr = new Arr();
        var values = GetValues();
        GetKeys().ForEach((key, i) =>
            arr.Push(new Arr(key, values[i])));
        return arr;
    }

    public override string[] GetDynamicMemberNames()
    {
        return memory.Keys.ToArray();
    }

    public override string ToString()
    {
        return JSON.Stringify(memory);
    }
}