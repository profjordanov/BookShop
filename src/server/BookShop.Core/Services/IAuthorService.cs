using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core.Models.Authors;
using BookShop.Core.Models.Books;

namespace BookShop.Core.Services
{
    public interface IAuthorService
    {
        Task<int> Create(string firstName, string lastName);

        Task<AuthorDetailsServiceModel> Details(int id);

        Task<IEnumerable<BookWithCategoriesServiceModel>> BooksByAuthorId(int authorId);
    }
}