using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookShop.Business.Extensions;
using BookShop.Core;
using BookShop.Core.Models.Books;
using BookShop.Core.Models.Books.ServiceModels;
using BookShop.Core.Services;
using BookShop.Data.Entities;
using BookShop.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace BookShop.Business.Services
{
    public class BookService : IBookService
    {
        private const int BooksCount = 10;

        private readonly ApplicationDbContext _appContext;
        private readonly IAuthorService _authorService;

        public BookService(
            ApplicationDbContext appContext,
            IAuthorService authorService)
        {
            _appContext = appContext;
            _authorService = authorService;
        }

        public async Task<Option<BookDetailsServiceModel, Error>> GetById(int id)
        {
            var result = (await _appContext
                    .Books
                    .Where(b => b.Id == id)
                    .ProjectTo<BookDetailsServiceModel>()
                    .FirstOrDefaultAsync())
                .SomeNotNull();

            return result.Match(
                book => book.Some<BookDetailsServiceModel, Error>(),
                () => Option.None<BookDetailsServiceModel, Error>($"There is no book with ID:{id} .".ToError()));
        }

        public async Task<Option<IEnumerable<BookListingServiceModel>, Error>> GetBySearchTerm(string searchTerm)
        {
            var result = (await _appContext
                    .Books
                    .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                    .OrderBy(b => b.Title)
                    .Take(BooksCount)
                    .ProjectTo<BookListingServiceModel>()
                    .ToListAsync())
                .NoneWhen(books => !books.Any());

            return result.Match(
                books => books.Some<IEnumerable<BookListingServiceModel>, Error>(),
                () => Option.None<IEnumerable<BookListingServiceModel>, Error>($"There are no books which contain '{searchTerm}'.".ToError()));
        }

        public async Task<Option<BookDetailsServiceModel, Error>> CreateByModel(BookWithCategoriesRequestModel model)
            => await _authorService.Exists(model.AuthorId) ?
                (await Create(model)).Some<BookDetailsServiceModel, Error>() :
                    Option.None<BookDetailsServiceModel, Error>($"Author with {model.AuthorId} doesn't exists in our database!".ToError());

        private async Task<BookDetailsServiceModel> Create(BookWithCategoriesRequestModel model)
        {
            // Create book
            var book = new Book //TODO: Add to AutoMapper
            {
                AuthorId = model.AuthorId,
                Title = model.Title.Trim(),
                Description = model.Description.Trim(),
                Price = model.Price,
                Copies = model.Copies,
                Edition = model.Edition,
                ReleaseDate = model.ReleaseDate,
                AgeRestriction = model.AgeRestriction
            };

            // Add Categories
            if (!string.IsNullOrWhiteSpace(model.Categories))
            {
                // Get categories
                var categoryNames = model.Categories
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToHashSet();

                var existingCategories = await _appContext
                    .Categories
                    .Where(c => categoryNames
                        .Select(cn => cn.ToLower())
                        .Contains(c.Name.ToLower()))
                    .ToListAsync();

                var allCategories = new List<Category>(existingCategories);

                foreach (var categoryName in categoryNames)
                {
                    if (existingCategories.All(c => !string.Equals(c.Name, categoryName, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        var category = new Category { Name = categoryName };
                        _appContext.Categories.Add(category);
                        allCategories.Add(category);
                    }
                }

                await _appContext.SaveChangesAsync();

                // Add Categories to Book
                foreach (var category in allCategories)
                {
                    book.Categories.Add(new CategoryBook { CategoryId = category.Id });
                }
            }

            await _appContext.AddAsync(book);
            await _appContext.SaveChangesAsync();

            return Mapper.Map<BookDetailsServiceModel>(book);
        }
    }
}