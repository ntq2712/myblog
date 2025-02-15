using AutoMapper;
using blog.DTO.User;
using blog.Model;

namespace blog.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, CreateUser>().ReverseMap();
        }
    }
}