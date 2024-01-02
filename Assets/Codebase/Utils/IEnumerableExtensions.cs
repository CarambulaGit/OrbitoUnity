using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class IEnumerableExtensions
    {
        private static readonly Random _random = new Random();

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) =>
            collection == null || !collection.GetEnumerator().MoveNext();

        public static T MinByOrDefault<T, S>(this IEnumerable<T> source, Func<T, S> selector) where S : IComparable<S>
        {
            if (source.IsNullOrEmpty())
                return default(T);
            return source.Aggregate((e, n) => selector(e).CompareTo(selector(n)) < 0 ? e : n);
        }

        public static T GetRandom<T>(this IEnumerable<T> values) where T : class
        {
            if (values.IsNullOrEmpty())
                return null;

            var randomIndex = _random.Next(0, values.Count());
            return values.ElementAt(randomIndex);
        }

        public static T GetRandom<T>(this IEnumerable<T> values, params T[] excludes)
        {
            values = values.Where(
                v => excludes.Contains(v) == false
            );

            return values.GetRandom();
        }

        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> values)
        {
            List<T> result = values.ToList();
            result.RemoveAll(item => item.Equals(null));
            return result;
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values) action.Invoke(value);
        }
    }
}