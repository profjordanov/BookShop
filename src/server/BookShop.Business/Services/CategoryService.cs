using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookShop.Core;
using BookShop.Core.Models.Categories;
using BookShop.Core.Services;
using BookShop.Data.Entities;
using BookShop.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace BookShop.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _appDbContext;

        public CategoryService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Option<IEnumerable<CategoryServiceModel>, Error>> All()
        {
            var result = (await _appDbContext
                .Categories
                .ProjectTo<CategoryServiceModel>()
                .ToListAsync())
                .SomeNotNull();

            return result.Match(
                category => category.Some<IEnumerable<CategoryServiceModel>, Error>(),
                () => Option.None<IEnumerable<CategoryServiceModel>, Error>(new Error("There are no categories!")));
        }

        public async Task<Option<CategoryServiceModel, Error>> GetById(int id)
        {
            var result = (await _appDbContext
                    .Categories
                    .Where(c => c.Id == id)
                    .ProjectTo<CategoryServiceModel>()
                    .FirstOrDefaultAsync())
                .SomeNotNull();

            return result.Match(
                category => category.Some<CategoryServiceModel, Error>(),
                () => Option.None<CategoryServiceModel, Error>(new Error("There is no such category!")));
        }

        private async Task<bool> Exists(string name)
            => await _appDbContext
                .Categories
                .AnyAsync(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));

        private async Task<CategoryServiceModel> Create(string name)
        {
            var category = new Category
            {
                Name = name
            };

            await _appDbContext.Categories.AddAsync(category);
            await _appDbContext.SaveChangesAsync();

            return Mapper.Map<CategoryServiceModel>(category);
        }

        public async Task<Option<CategoryServiceModel, Error>> CreateByName(string name)
        {
            return await Exists(name) ?
                Option.None<CategoryServiceModel, Error>(new Error($"Category '{name}' already exists.")) :
                (await Create(name)).Some<CategoryServiceModel, Error>();
        }
    }
}