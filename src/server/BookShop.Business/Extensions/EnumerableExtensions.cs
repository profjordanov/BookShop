using System.Collections.Generic;

namespace BookShop.Business.Extensions
{
    public static class EnumerableExtensions
    {
        public static ISet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
            => new HashSet<T>(enumerable);
    }
}