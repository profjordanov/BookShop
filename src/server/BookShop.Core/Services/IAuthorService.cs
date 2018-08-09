using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core.Models.Authors;
using BookShop.Core.Models.Authors.ServiceModels;
using BookShop.Core.Models.Books;
using BookShop.Core.Models.Books.ServiceModels;
using Optional;

namespace BookShop.Core.Services
{
    public interface IAuthorService
    {
        Task<AuthorDetailsServiceModel> Create(AuthorRequestModel model);

        Task<Option<AuthorDetailsServiceModel, Error>> GetById(int id);

        Task<Option<IEnumerable<BookWithCategoriesServiceModel>, Error>> BooksByAuthorId(int authorId);

        Task<bool> Exists(int id);
    }
}