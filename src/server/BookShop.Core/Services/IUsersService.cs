using System.Threading.Tasks;
using BookShop.Core.Models;
using Optional;

namespace BookShop.Core.Services
{
    public interface IUsersService
    {
        Task<Option<JwtModel, Error>> Login(LoginUserModel model);

        Task<Option<UserModel, Error>> Register(RegisterUserModel model);
    }
}
