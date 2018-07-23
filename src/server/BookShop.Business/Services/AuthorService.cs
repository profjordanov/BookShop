using System;
using System.Threading.Tasks;
using BookShop.Core.Services;
using BookShop.Data.Entities;
using BookShop.Data.EntityFramework;

namespace BookShop.Business.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _appContext;

        public AuthorService(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<int> Create(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            await _appContext.Authors.AddAsync(author);
            await _appContext.SaveChangesAsync();

            return author.Id;
        }
    }
}