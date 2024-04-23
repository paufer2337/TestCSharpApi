namespace Dyndata;
using Newtonsoft.Json;
using System.Globalization;
using FracturedJson;

// Parse JSON with a focus on creating Objs and Arrs (via Utils.TryToObjOrArr)
// Stringify to JSON with an identation option
// that also turns on syntax highlighting of the JSON
// + provide a Log-specific variant for Factory.Log

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

    public static string StringifyForLog(dynamic obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        return Humane(json, true);
    }

    private static string Humane(string json, bool forLog = false)
    {
        var oldCulture = CultureInfo.DefaultThreadCurrentCulture;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        var opts = new FracturedJsonOptions() { MaxTotalLineLength = 90 };
        var formatter = new Formatter() { Options = opts };
        var output = formatter.Reformat(json, 0).Trim();
        CultureInfo.DefaultThreadCurrentCulture = oldCulture;
        output = forLog ? ForLog(output) : output;
        output = highlight ? Colorize(output) : output;
        output = forLog ? ForLog(output, false) : output;
        output = highlight ? output.Replace("!%&€", "\u001b[") : output;
        return output;
    }

    private static string Colorize(string json)
    {
        var c = Obj(new
        {
            propNames = 186, // green/yellow
            booleans = 180,  // orange
            strings = 250,   // off-white
            numbers = 153,   // blue
            brackets = 245,  // gray
            _default = 245,  // gray
            reset = 0        // reset code
        });

        ((Arr)c.GetKeys()).ForEach(x => c[x] = "!%&€38;5;" + c[x] + "m");
        c.reset += c._default;
        var rr = RemoveAndReinsertStrings(json);

        var x = Regex.Replace(rr.str, @"([\d\.]+)", $"{c.numbers}$1{c.reset}");
        x = Regex.Replace(x, "\"([^\"]*)\":", $"{c.propNames}__quote__$1__quote__:{c.reset}");
        x = Regex.Replace(x, "\"([^\"]*)\"", $"{c.strings}\"$1\"{c.reset}");
        x = x.Replace("__quote__", "\"");
        x = x.Replace("true", $"{c.booleans}true{c.reset}");
        x = x.Replace("false", $"{c.booleans}false{c.reset}");
        x = RemoveAndReinsertStrings(x, rr.extracted).str;

        return c._default + x.Replace("\n", "\n" + c._default);
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

    private static string ForLog(string json, bool beforeColorize = true)
    {
        var x = beforeColorize ?
            json.Trim('[', ']').Trim().Replace("\n    ", "\n") : json;
        var output = "";
        var inArr = 0;
        var lastChar = 'x';
        foreach (var c in x)
        {
            if ((c == '[' || c == '{') && lastChar != '\\') { inArr++; }
            if ((c == ']' || c == '}') && lastChar != '\\') { inArr--; }
            output += beforeColorize ?
                inArr == 0 && c == ',' ? "" : c :
                inArr == 0 && c == '"' && lastChar != '\\' ? "" : c;
            lastChar = c;
        }
        return output;
    }
}