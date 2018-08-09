using System.Linq;
using AutoMapper;
using BookShop.Core.Models.Books.ServiceModels;
using BookShop.Data.Entities;

namespace BookShop.Core.Mappings
{
    public class BooksMapping : Profile
    {
        public BooksMapping()
        {
            CreateMap<Book, BookWithCategoriesServiceModel>(MemberList.Destination)
                .ForMember(b => b.Categories, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Name)));

            CreateMap<Book, BookDetailsServiceModel>(MemberList.Destination)
                .ForMember(b => b.Categories, cfg => cfg
                    .MapFrom(b => b.Categories.Select(c => c.Category.Name)))
                .ForMember(b => b.Author, cfg => cfg
                    .MapFrom(b => $"{b.Author.FirstName} {b.Author.LastName}"));

            CreateMap<Book, BookListingServiceModel>(MemberList.Destination);
        }
    }
}