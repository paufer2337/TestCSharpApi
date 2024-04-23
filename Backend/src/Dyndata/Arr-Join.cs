namespace Dyndata;
// Arr: Join method

public partial class Arr
{
    public string Join(string glue = ",")
    {
        return Reduce(
            (a, c, i) => a + c + (i == Length - 1 ? "" : glue),
            ""
        );
    }
}