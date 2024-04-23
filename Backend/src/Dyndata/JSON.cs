namespace Dyndata;
using Newtonsoft.Json;
using System.Globalization;
using FracturedJson;

// Parse JSON with a focus on creating Objs and Arrs (via Utils.TryToObjOrArr)
// Stringify to JSON with an identation option
// that also turns on syntax highlighting of the JSON

public static class JSON
{
    private static bool highlight = true;

    public static bool Highlight
    {
        get { return highlight; }
        set { highlight = value; }
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

    public static string Stringify(dynamic obj, bool indented = false)
    {
        var json = JsonConvert.SerializeObject(obj);
        return !indented ? json : Humane(json);
    }

    private static string Humane(string json)
    {
        var oldCulture = CultureInfo.DefaultThreadCurrentCulture;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        var opts = new FracturedJsonOptions() { MaxTotalLineLength = 90 };
        var formatter = new Formatter() { Options = opts };
        var output = formatter.Reformat(json, 0);
        CultureInfo.DefaultThreadCurrentCulture = oldCulture;
        return highlight ? Colorize(output) : output;
    }

    private static string Colorize(string json)
    {
        var WHITE = "\u001b[38;5;250m";
        var ORANGE = "\u001b[38;5;221m";
        var GREEN = "\u001b[38;5;150m";
        var BLUE = "\u001b[38;5;117m";
        var GREY = "\u001b[38;5;245m";
        var DEFAULT = GREY;
        var RESET = "\u001b[0m" + DEFAULT;

        var rr = RemoveAndReinsertStrings(json);

        var x = Regex.Replace(rr.str, @"([\d\.]+)", $"{BLUE}$1{RESET}");
        x = Regex.Replace(x, "\"([^\"]*)\":", $"{WHITE}__quote__$1__quote__:{RESET}");
        x = Regex.Replace(x, "\"([^\"]*)\"", $"{GREEN}\"$1\"{RESET}");
        x = x.Replace("__quote__", "\"");
        x = x.Replace("true", $"{ORANGE}true{RESET}");
        x = x.Replace("false", $"{ORANGE}false{RESET}");
        x = RemoveAndReinsertStrings(x, rr.extracted).str;

        return DEFAULT + x.Replace("\n", "\n" + DEFAULT);
    }

    private static dynamic RemoveAndReinsertStrings(string json, string reinsert = "")
    {
        var isRe = reinsert != "";
        var inString = false;
        var lastChar = 'x';
        var str = "";
        var extracted = "";
        var co = 0;
        foreach (var c in json)
        {
            if (inString) { }
            if (c == '"' && lastChar != '\\')
            {
                inString = !inString;
                if (inString) { str += c; continue; }
            }
            str += inString ? isRe ? reinsert[co++] : "_" : c;
            extracted += inString ? c : "";
            lastChar = c;
        }
        return new { str, extracted };
    }
}