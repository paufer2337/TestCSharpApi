using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

public static class Debug
{
    public static bool on = false;

    public static void Log(string type, object data)
    {
        if (!on) { return; }

        if (type == "route")
        {
            var req = ((HttpContext)data).Request;
            var route = req.Method + " " + req.Path;
            Console.WriteLine("\n" + route);
        }

        if (type == "sql")
        {
            var d = new DynObject(data);
            var sql = Regex.Replace(d.GetStr("sql"), @"\s+", " ");

            // Ignore logging for: 
            // 1) Sql queries to session
            // 2) The SELECT to determine insertId after an INSERT
            if (
                sql.Contains("FROM sessions") ||
                sql.Contains("INTO sessions") ||
                sql.Contains("UPDATE sessions") ||
                sql.Contains("__insertId")
            ) { return; }

            Console.WriteLine("  " + sql);
            var p = ((Newtonsoft.Json.Linq.JArray)
                d.Get("parameters")).ToArray();
            var longestKey = p
                .Select((x, i) => i % 2 == 1 ? 0 : ((string?)x)!.Length)
                .ToArray().DefaultIfEmpty(0).Max(x => x);
            for (var i = 0; i < p.Length; i += 2)
            {
                var key = ((string?)p[i])!.PadRight(longestKey);
                Console.WriteLine("    $" + key + " = " + p[i + 1]);
            }
        }

        if (type == "sqlError")
        {
            Console.WriteLine("  SQL Error: " + data);
        }
    }
}