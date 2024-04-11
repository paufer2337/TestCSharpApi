namespace LinqExtend
{

    public static class LinqExtend
    {
        private static dynamic _(dynamic source, dynamic result)
        {
            return Arr._(result);
        }

        public static dynamic XMap<T>(
            this IEnumerable<T> arr, Func<T, dynamic> func
        ) => typeof(T);

        public static dynamic AMap<T>(
            this IEnumerable<T> arr, Func<T, dynamic> func
        ) => _(arr, arr.Select(func));

        public static dynamic XFilter<T>(
            this IEnumerable<T> arr, Func<T, bool> func
        ) => _(arr, arr.Where(func));

        public static dynamic XFind<T>(
            this IEnumerable<T> arr, Func<T, dynamic> func
        ) => arr.FirstOrDefault(x => func(x))!;

        public static dynamic FindLast<T>(
           this IEnumerable<T> arr, Func<T, dynamic> func
       ) => arr.LastOrDefault(x => func(x))!;
    }
}