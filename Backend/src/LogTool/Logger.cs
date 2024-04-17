namespace LogTool;
public static class Logger
{
    public static void Log(params object[] paras)
    {
        Console.WriteLine(String.Join(" ", paras
            .Select(x => x != null ? x : "null"))
            .Replace("'", "\"")
        );
    }
}