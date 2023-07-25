using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ore2Lib
{
    public static class EnumerableExtensions
    {
        public static string ToJoinedString<T>(this IEnumerable<T> source) {
            return $"[{string.Join(", ", source)}]";
        }

        public static string ToStringRecursively(this IEnumerable source) {
            var builder = new StringBuilder();
            builder.Append("[");

            foreach (object element in source) {
                if (element is IEnumerable enumerable) {
                    builder.Append(enumerable.ToStringRecursively());
                } else {
                    builder.Append(element);
                }
                builder.Append(", ");
            }

            if (builder.Length > 1) {
                builder.Remove(builder.Length - 2, 2);
            }

            builder.Append("]");
            return builder.ToString();
        }
    }
}
