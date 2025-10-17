using AutoMapper;
using blog.DTO.MyProfile;
using blog.DTO.User;
using blog.Model;

namespace blog.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUser>().ReverseMap();
            CreateMap<User, CUser>().ReverseMap();
            CreateMap<MyProfile, CMyProfile>().ReverseMap();
        }
    }
}