public static class Con
{
    public static void Log(params object[] paras)
    {
        Console.WriteLine(String.Join(" ", paras
            .Select(x => x != null ? x : "null"))
            .Replace("'", "\"")
        );
    }
}