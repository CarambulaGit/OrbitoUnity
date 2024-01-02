using System;
using System.Collections.Generic;

namespace Utils
{
    public static class ListExtension
    {
        public static int IndexOf<T>(this IList<T> collection, Predicate<T> predicate)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (predicate?.Invoke(collection[i]) ?? false)
                {
                    return i;
                }
            }

            return -1;
        }

        public static void FillWith<T>(this IList<T> list, T value)
        {
            for (var i = 0; i < list.Count; i++) list[i] = value;
        }

        public static void FillWith<T>(this IList<T> list, Func<int, T> replaceWith)
        {
            for (var i = 0; i < list.Count; i++) list[i] = replaceWith.Invoke(i);
        }
    }
}