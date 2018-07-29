using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BookShop.Core.Models.Authors;
using BookShop.Core.Models.Books;
using BookShop.Core.Services;
using BookShop.Data.Entities;
using BookShop.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AuthorDetailsServiceModel> Details(int id)
            => await _appContext
                .Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<BookWithCategoriesServiceModel>> BooksByAuthorId(int authorId)
            => await _appContext
                .Books
                .Where(b => b.AuthorId == authorId)
                .ProjectTo<BookWithCategoriesServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await _appContext.Authors.AnyAsync(a => a.Id == id);
    }
}