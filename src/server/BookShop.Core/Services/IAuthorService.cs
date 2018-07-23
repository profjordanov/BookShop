using System.Threading.Tasks;

namespace BookShop.Core.Services
{
    public interface IAuthorService
    {
        Task<int> Create(string firstName, string lastName);
    }
}