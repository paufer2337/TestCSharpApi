using System.Dynamic;
using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using Newtonsoft.Json;

public record FuncWrapper(
    string Action,
    Func<object, object> FuncObjObj = null!,
    Func<object, bool> FuncObjBool = null!,
    Func<object, int, object> FuncObjIntObj = null!
);

public partial class Dynamic : DynamicObject
{

    private Dictionary<string, object> propMemory = new Dictionary<string, object>();
    private List<object> arrMemory = new List<object>();

    public static object jMap(Dynamic dynObj, Expression expression)
    {
        return dynObj.Map(expression.ToReadableString());
    }

    public static FuncWrapper xMap(Func<object, object> func)
    {
        return new FuncWrapper("map", func);
    }

    public static FuncWrapper xMap(Func<object, int, object> func)
    {
        return new FuncWrapper("mapI", null!, null!, func);
    }

    public static FuncWrapper Filter(Func<object, bool> func)
    {
        return new FuncWrapper("filter", null!, func);
    }

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

    public DynamicObject Merge(object values)
    {
        if (values is IEnumerable<object>)
        {
            foreach (var value in (IEnumerable<object>)values)
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

    public override string ToString()
    {
        return JsonConvert.SerializeObject(
            propMemory.Count() > 0 ? propMemory : arrMemory, Formatting.Indented
        );
    }

    public List<object> ToList()
    {
        return arrMemory.ToList();
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