namespace Dyndata;
using System.Dynamic;
using System.Reflection;

#nullable disable
public class Consol : Dynamic
{
    public override bool TryInvokeMember(

        InvokeMemberBinder binder, object[] args, out dynamic result
    )
    {
        result = null;
        try
        {
            result = typeof(Console).InvokeMember(
                binder.Name,
                BindingFlags.InvokeMethod,
                null, null,
                args
            );
        }
        catch (Exception) { return false; }
        return true;
    }
}