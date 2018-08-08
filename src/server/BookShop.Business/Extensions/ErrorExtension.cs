using BookShop.Core;

namespace BookShop.Business.Extensions
{
    public static class ErrorExtension
    {
        public static Error ToError(this string message)
            => new Error(message);
    }
}