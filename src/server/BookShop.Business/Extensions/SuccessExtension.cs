using BookShop.Core;

namespace BookShop.Business.Extensions
{
    public static class SuccessExtension
    {
        public static Success ToSuccess(this string message)
            => new Success(message);
    }
}