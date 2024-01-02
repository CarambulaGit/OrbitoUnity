using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Utils
{
    public static class RectArrayExtension
    {
        public static string GetString<T>(this T[,] array, string separator = ", ", string replaceNullWith = "null")
        {
            var sb = new StringBuilder();
            for (var y = 0; y < array.GetLength(0); y++)
            {
                sb.Append("[ ");
                for (var x = 0; x < array.GetLength(1); x++)
                {
                    var str = array[y, x]?.ToString() ?? replaceNullWith;
                    sb.Append(x == array.GetLength(1) - 1 ? $"{str}" : $"{str}{separator}");
                }

                sb.Append(" ]\n");
            }

            return sb.ToString();
        }

        public static void ForEach<T>(this T[,] array, Action<T> toDo)
        {
            foreach (var elem in array)
            {
                toDo(elem);
            }
        }

        public static IEnumerable<T> Where<T>(this T[,] array, Predicate<T> toDo)
        {
            foreach (var elem in array)
            {
                if (toDo(elem))
                {
                    yield return elem;
                }
            }
        }

        public static void Clear<T>(this T[,] array, T elem) => ClearInternal(array, elem, false);

        public static void ClearAll<T>(this T[,] array, T elem) => ClearInternal(array, elem, true);

        private static void ClearInternal<T>(T[,] array, T elem, bool all)
        {
            var yMax = array.GetLength(0);
            var xMax = array.GetLength(1);
            for (var i = 0; i < yMax; i++)
            {
                for (var j = 0; j < xMax; j++)
                {
                    if (array[i, j] == null || !array[i, j].Equals(elem)) continue;
                    array[i, j] = default;
                    if (!all) return;
                }
            }
        }

        public static bool Any<T>(this T[,] array, Predicate<T> predicate)
        {
            foreach (var elem in array)
            {
                if (predicate(elem))
                {
                    return true;
                }
            }

            return false;
        }

        public static void FillWith<T>(this T[,] array, Func<T> elemToFill)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            for (var j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = elemToFill.Invoke();
            }
        }

        public static List<T> ToList<T>(this T[,] array)
        {
            return array.Cast<T>().ToList();
        }

        public static T ByListIndex<T>(this T[,] array, int index)
        {
            if (index >= array.Length)
            {
                throw new Exception($"{index} out of range");
            }

            var point = array.PointByListIndex(index);
            return array[point.y, point.x];
        }

        public static Vector2Int PointByListIndex<T>(this T[,] array, int index)
        {
            if (index >= array.Length)
            {
                throw new Exception($"{index} out of range");
            }

            var xLen = array.GetLength(1);
            var y = index / xLen;
            var x = index % xLen;
            return new Vector2Int(x, y);
        }

        public static int GetListIndexOf<T>(this T[,] array, T elem)
        {
            var index = 0;
            var yLen = array.GetLength(0);
            var xLen = array.GetLength(1);
            for (var y = 0; y < yLen; y++)
            {
                for (var x = 0; x < xLen; x++, index++)
                {
                    if (array[y, x].Equals(elem))
                    {
                        return index;
                    }
                }
            }

            return -1;
        }

        public static int GetListIndexOf<T>(this T[,] array, Vector2Int point)
        {
            var xLen = array.GetLength(1);
            var index = point.y * xLen + point.x;
            if (index >= array.Length)
            {
                throw new Exception($"{point} out of range");
            }

            return index;
        }

        public static T[,] GetRectArray<T>(this List<T> list)
        {
            var size = (int) Math.Sqrt(list.Count);
            var result = new T[size, size];
            for (var i = 0; i < list.Count; i++)
            {
                var point = result.PointByListIndex(i);
                result[point.y, point.x] = list[i];
            }

            return result;
        }

        public static ref T GetRef<T>(this T[,] array, Vector2Int point) => ref array[point.y, point.x];
        public static T Get<T>(this T[,] array, Vector2Int point) => array[point.y, point.x];
    }
}