using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Business.Extensions;
using BookShop.Core.Services;
using BookShop.Data.Entities;
using BookShop.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Business.Services
{
    public class BookService : IBookService
    {
        private const int BooksCount = 10;

        private readonly ApplicationDbContext _appContext;

        public BookService(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime? releaseDate,
            int authorId,
            string categories)
        {
            // Create book
            var book = new Book
            {
                AuthorId = authorId,
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                ReleaseDate = releaseDate,
                AgeRestriction = ageRestriction
            };

            // Add Categories
            if (!string.IsNullOrWhiteSpace(categories))
            {
                // Get categories
                var categoryNames = categories
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
                    if (!existingCategories.Any(c => c.Name.ToLower() == categoryName.ToLower()))
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

            return book.Id;
        }
    }
}