namespace Dyndata;
using Newtonsoft.Json;

public static class JSON
{
    public static string Stringify(dynamic obj, bool indented = false)
    {
        return !indented ? JsonConvert.SerializeObject(obj) :
         JsonConvert.SerializeObject(obj, Formatting.Indented);
    }

    public static dynamic Parse(string json)
    {
        dynamic parsed;
        try
        {
            parsed = JsonConvert.DeserializeObject
                <Dictionary<string, object>>(json)!;
        }
        catch (Exception)
        {
            parsed = JsonConvert.DeserializeObject
                <Dictionary<string, object>[]>(json)!;
        }
        return Utils.TryToObjOrArr(parsed);
    }
}