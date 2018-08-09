using System.Linq;
using AutoMapper;
using BookShop.Core.Models.Authors.ServiceModels;
using BookShop.Data.Entities;

namespace BookShop.Core.Mappings
{
    public class AuthorsMapping : Profile
    {
        public AuthorsMapping()
        {
            CreateMap<Author, AuthorDetailsServiceModel>(MemberList.Destination)
                .ForMember(a => a.Books, cfg => cfg
                    .MapFrom(a => a.Books.Select(b => b.Title)));

        }
    }
}