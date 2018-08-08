using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core.Models.Categories;
using Optional;

namespace BookShop.Core.Services
{
    public interface ICategoryService
    {
        Task<Option<IEnumerable<CategoryServiceModel>, Error>> All();

        Task<Option<CategoryServiceModel, Error>> GetById(int id);

        Task<Option<CategoryServiceModel, Error>> CreateByName(string name);

        Task<Option<CategoryServiceModel, Error>> UpdateByModel(CategoryServiceModel model);
    }
}