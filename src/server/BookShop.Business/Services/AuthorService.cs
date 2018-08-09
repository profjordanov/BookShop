using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookShop.Business.Extensions;
using BookShop.Core;
using BookShop.Core.Models.Authors;
using BookShop.Core.Models.Authors.ServiceModels;
using BookShop.Core.Models.Books.ServiceModels;
using BookShop.Core.Services;
using BookShop.Data.Entities;
using BookShop.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace BookShop.Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _appContext;

        public AuthorService(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Option<AuthorDetailsServiceModel, Error>> GetById(int id)
        {
            var result = (await _appContext
                    .Authors
                    .Where(a => a.Id == id)
                    .ProjectTo<AuthorDetailsServiceModel>()
                    .FirstOrDefaultAsync())
                .SomeNotNull();

            return result.Match(
                authors => authors.Some<AuthorDetailsServiceModel, Error>(),
                () => Option.None<AuthorDetailsServiceModel, Error>($"There is no author with ID:{id}!".ToError()));
        }

        public async Task<AuthorDetailsServiceModel> Create(AuthorRequestModel model)
        {
            var author = new Author
            {
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim()
            };

            await _appContext.Authors.AddAsync(author);
            await _appContext.SaveChangesAsync();

            return Mapper.Map<AuthorDetailsServiceModel>(author);
        }

        public async Task<Option<IEnumerable<BookWithCategoriesServiceModel>, Error>> BooksByAuthorId(int authorId)
        {
            var result = (await _appContext
                    .Books
                    .Where(b => b.AuthorId == authorId)
                    .ProjectTo<BookWithCategoriesServiceModel>()
                    .ToListAsync())
                .NoneWhen(b => !b.Any());

            return result.Match(
                books => books.Some<IEnumerable<BookWithCategoriesServiceModel>, Error>(),
                () => Option.None<IEnumerable<BookWithCategoriesServiceModel>, Error>($"There are no books from author with ID:{authorId}.".ToError()));
        }

        public async Task<bool> Exists(int id)
            => await _appContext.Authors.AnyAsync(a => a.Id == id);
    }
}