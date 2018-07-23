using BookShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BookShop.Business.Tests
{
    public static class IdentityMocksProvider
    {
        public static Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
