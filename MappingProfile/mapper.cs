using AutoMapper;
using WebApplication1.DTO;
using WebApplication1.Model;

namespace WebApplication1.MappingProfile
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<  UserDto,User>(); ;
            CreateMap<CourseDto,Course>();
            CreateMap<Course,CourseDto>(); ;
        }
    }
}
