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
        Con.Log("SOURCES", JSON.Stringify(sources));
        foreach (object source in sources)
        {
            foreach (var prop in source.GetType().GetProperties())
            {
                var key = prop.Name;
                var value = prop.GetValue(source);
                if (key.StartsWith("SPREAD"))
                {
                    Con.Log("SPREAD MERGE", value);
                    Merge(value!);
                    continue;
                }
                Con.Log("WRITTEN KEY", key);
                memory[key] = value!;
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

    public override string[] GetDynamicMemberNames()
    {
        return memory.Keys.ToArray();
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

    public override string ToString()
    {
        return JSON.Stringify(memory);
    }
}