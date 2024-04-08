using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AgileObjects.ReadableExpressions;

//using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.Data;

namespace ExtensionMethods
{
    public static class StringUtils
    {
        public static string Regplace(
            this string str, string pattern, string replacement
        )
        {
            return Regex.Replace(str, pattern, replacement);
        }

        public static bool Match(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }

        public static string Join(this string[] strArray, string glue)
        {
            return string.Join(glue, strArray);
        }

        public static int IndexOf(this string[] strArray, string find)
        {
            return Array.IndexOf(strArray, find);
        }
    }

    // Instead of DynObject extend objects? (Maybe - this is a quick test...)
    public static class ObjectUtils
    {

        /*public static object GetKeys(this object obj)
        {
            var descs = new List<String>();
            foreach (var desc in TypeDescriptor.GetProperties(obj))
            {
                descs.Add(((PropertyDescriptor)desc).DisplayName);
            }
            Console.WriteLine(JsonConvert.DeserializeObject(JSON.Stringify(descs)).GetType());
            return JsonConvert.DeserializeObject(JSON.Stringify(descs))!;
        }

        public static object? GetObj(this object obj, string prop)
        {
            var properties = TypeDescriptor.GetProperties(obj);
            var desc = properties.Find(prop, false)!;
            return desc != null ? desc.GetValue(obj)! : null;
        }*/

        public static JObject Dynamic(this object obj)
        {
            return (JObject)JsonConvert.DeserializeObject(JSON.Stringify(obj))!;
        }

        public static void Delete(this JObject obj, string prop)
        {
            obj.Remove(prop);
        }

        public static void Set(this JObject obj, string prop, object value)
        {
            obj.Delete(prop);
            obj.Add(prop, new JValue(value));
        }

        public static JObject GetObj(this JObject obj, string prop)
        {
            return obj.GetValue(prop)!.Dynamic();
        }

        public static string GetStr(this JObject obj, string prop)
        {
            return (string)obj.GetValue(prop)!;
        }

        public static int GetInt(this JObject obj, string prop)
        {
            return (int)obj.GetValue(prop)!;
        }

        public static decimal GetDec(this JObject obj, string prop)
        {
            return (decimal)obj.GetValue(prop)!;
        }

        public static double GetDbl(this JObject obj, string prop)
        {
            return (double)obj.GetValue(prop)!;
        }

        public static bool GetBool(this JObject obj, string prop)
        {
            return (bool)obj.GetValue(prop)!;
        }

    }

    public static class ArrayUtils
    {

        public static JArray Dynamic(this Array arr)
        {
            return (JArray)JsonConvert.DeserializeObject(JSON.Stringify(arr))!;
        }

        public static int Length(this JArray arr)
        {
            return arr.Count();
        }

        public static JArray Splice(
            this JArray arr, int start, int deleteCount, params object[] values
        )
        {
            start = start < 0 ? arr.Count() - start : start;
            start = start < 0 ? 0 : start;
            var removed = new JArray();
            for (var i = 0; i < deleteCount && arr.Length() - 1 >= start; i++)
            {
                removed.Add(arr[start]);
                arr.RemoveAt(start);
            }
            for (var i = values.Length - 1; i >= 0; i--)
            {
                arr.Insert(start, values[i].Dynamic());
            }
            return removed;
        }

        public static int Push(this JArray arr, params object[] values)
        {
            arr.Splice(arr.Length(), 0, values);
            return arr.Length();
        }

        public static int Unshift(this JArray arr, params object[] values)
        {
            arr.Splice(0, 0, values);
            return arr.Length();
        }

        public static JObject Pop(this JArray arr)
        {
            return arr.Splice(arr.Length() - 1, 1)[0].Dynamic();
        }

        public static JObject Shift(this JArray arr)
        {
            return arr.Splice(0, 1)[0].Dynamic();
        }

        public static JObject GetObj(this JArray arr, int index, string prop)
        {
            return arr[index].Dynamic().GetObj(prop);
        }

        public static string GetStr(this JArray arr, int index, string prop)
        {
            return arr[index].Dynamic().GetStr(prop);
        }

        public static int GetInt(this JArray arr, int index, string prop)
        {
            return arr[index].Dynamic().GetInt(prop);
        }

        public static decimal GetDec(this JArray arr, int index, string prop)
        {
            return arr[index].Dynamic().GetDec(prop);
        }

        public static double GetDbl(this JArray arr, int index, string prop)
        {
            return arr[index].Dynamic().GetDbl(prop);
        }

        public static bool GetBool(this JArray arr, int index, string prop)
        {
            return arr[index].Dynamic().GetBool(prop);
        }

        private static void DynThrower(object obj, string methodName)
        {
            if (obj as Dynamic != null) { return; }
            throw new Exception($"The {methodName} method is meant " +
                "for use with Dynamic instances only!");
        }

        public static Dynamic Map(this object obj, Func<object, object> lambda)
        {
            DynThrower(obj, "Map");
            return new Dynamic(((Dynamic)obj).ToArray().Select(lambda));
        }

        public static Dynamic Map(this object obj, Func<object, int, object> lambda)
        {
            DynThrower(obj, "Map");
            return new Dynamic(((Dynamic)obj).ToArray().Select(lambda));
        }

        public static Dynamic Filter(this object obj, Func<object, bool> lambda)
        {
            DynThrower(obj, "Filter");
            return new Dynamic(((Dynamic)obj).ToArray().Where(lambda));
        }

        public static Dynamic Filter(this object obj, Func<object, int, bool> lambda)
        {
            DynThrower(obj, "Filter");
            return new Dynamic(((Dynamic)obj).ToArray().Where(lambda));
        }

        public static String _(this Expression expression)
        {
            return expression.ToReadableString();
        }

    }
}