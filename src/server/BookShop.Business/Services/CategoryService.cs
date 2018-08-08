using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookShop.Business.Extensions;
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
                .SomeNotNull(); //TODO: TEST OVER HERE

            return result.Match(
                category => category.Some<IEnumerable<CategoryServiceModel>, Error>(),
                () => Option.None<IEnumerable<CategoryServiceModel>, Error>("There are no categories!".ToError()));
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



        public async Task<Option<CategoryServiceModel, Error>> CreateByName(string name)
        {
            return await Exists(name) ?
                Option.None<CategoryServiceModel, Error>(new Error($"Category '{name}' already exists.")) :
                (await Create(name)).Some<CategoryServiceModel, Error>();
        }

        public async Task<Option<CategoryServiceModel,Error>> UpdateByModel(CategoryServiceModel model)
        {
            if (await Exists(model.Id))
            {
                if (await Exists(model.Name))
                {
                    return Option.None<CategoryServiceModel, Error>($"Category '{model.Name}' already exists.".ToError());
                }
                else
                {
                    return (await Update(model)).Some<CategoryServiceModel, Error>();
                }
            }
            else
            {
                return Option.None<CategoryServiceModel, Error>($"Category with ID: {model.Id} does not exists.".ToError());
            }
        }

        public async Task<Option<Success, Error>> DeleteById(int id)
            => await Exists(id) ?
                (await Delete(id)).Some<Success, Error>() :
                    Option.None<Success, Error>($"Category with ID: {id} does not exists.".ToError());

        private async Task<bool> Exists(int id)
            => await _appDbContext
                .Categories
                .AnyAsync(c => c.Id == id);

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

        private async Task<CategoryServiceModel> Update(CategoryServiceModel model)
        {
            var category = await _appDbContext.Categories.FindAsync(model.Id);
            category.Name = model.Name.Trim();
            await _appDbContext.SaveChangesAsync();
            return Mapper.Map<CategoryServiceModel>(category);
        }

        private async Task<Success> Delete(int id)
        {
            var category = await _appDbContext.Categories.FindAsync(id);
            _appDbContext.Remove(category);
            await _appDbContext.SaveChangesAsync();
            return $"Category with ID: {id} was successfully deleted.".ToSuccess();
        }
    }
}