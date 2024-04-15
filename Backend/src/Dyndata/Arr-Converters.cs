namespace Dyndata;
using Newtonsoft.Json;

// Arr: Converters
// (convert ToString/JSON, ToArray and ToList)

public partial class Arr
{
    public override String ToString()
    {
        return JsonConvert.SerializeObject(memory)
        .Replace(",{", ",\n  {")
        .Replace("[{", "[\n  {")
        .Replace("}]", "}\n]")
        .Replace(",[", ",\n  [")
        .Replace("[[", "[\n  [")
        .Replace("]]", "]\n]");
    }

    public List<object> ToList()
    {
        return memory.ToList();
    }

    public dynamic[] ToArray()
    {
        return memory.ToArray();
    }
}