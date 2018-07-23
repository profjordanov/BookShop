using AutoMapper;
using BookShop.Core.Models;
using BookShop.Data.Entities;

namespace BookShop.Core.Mappings
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<User, UserModel>(MemberList.Destination);

            CreateMap<RegisterUserModel, User>(MemberList.Source)
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.Email))
                .ForSourceMember(s => s.Password, opts => opts.Ignore());
        }
    }
}
