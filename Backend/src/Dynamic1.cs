using System.Dynamic;
using System.Collections;
using Newtonsoft.Json;
using System.Linq.Expressions;

public partial class Dynamic : DynamicObject
{

    private Dictionary<string, object> propMemory = new Dictionary<string, object>();
    private List<object> arrMemory = new List<object>();

    public Dynamic() { }

    public Dynamic(object values)
    {
        Merge(values);
    }

    public object this[string propertyName]
    {
        get { return propMemory[propertyName]; }
        set { AddProperty(propertyName, value); }
    }

    public object this[int index]
    {
        get { return arrMemory[index]; }
        set { arrMemory[index] = value; }
    }

    private void AddProperty(string name, object value)
    {
        propMemory[name] = value;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        return propMemory.TryGetValue(binder.Name, out result!);
    }

    public override bool TrySetMember(SetMemberBinder binder, object? value)
    {
        AddProperty(binder.Name, value!);
        return true;
    }

    public bool Delete(string key)
    {
        propMemory.Remove(key);
        return true;
    }

    public bool HasKey(string key)
    {
        return propMemory.ContainsKey(key);
    }

    public DynamicObject Merge(object values)
    {
        try
        {
            if (values as IEnumerable != null && !(values is String))
            {
                foreach (var value in (values as IEnumerable)!)
                {
                    arrMemory.Add(value);
                }
            }
            else
            {
                var json = JsonConvert.SerializeObject(values);
                var dict = JsonConvert.DeserializeObject
                    <Dictionary<string, dynamic>>(json);
                foreach (var item in dict!)
                {
                    propMemory[item.Key] = item.Value;
                }
            }
            return this;
        }
        catch (Exception)
        {
            throw new Exception(
                "Can not create/merge an instance of Dynamic from ("
                + values.GetType().Name + ")"
                + JsonConvert.SerializeObject(values)
            );
        }
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(
            propMemory.Count() > 0 ? propMemory : arrMemory, Formatting.Indented
        );
    }

    public object[] ToArray()
    {
        return arrMemory.ToArray();
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>(propMemory);
    }

}