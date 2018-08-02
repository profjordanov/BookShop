using AutoMapper;
using BookShop.Core.Models.Categories;
using BookShop.Data.Entities;

namespace BookShop.Core.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryServiceModel>(MemberList.Destination);
        }
    }
}