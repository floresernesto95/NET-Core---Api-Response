using AutoMapper;
using Project_Courses.Dtos;
using Project_Courses.Models;

namespace Project_Courses
{
    // Define a class that inherits from AutoMapper's 'Profile'. 
    // 'Profile' is a base class provided by AutoMapper to configure specific mappings between types.
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            // Define a mapping configuration from the 'Course' entity model to the 'CourseDto' DTO.
            // This mapping will be used when converting a 'Course' instance to a 'CourseDto' instance.
            CreateMap<Course, CourseDto>();

            // Define a mapping configuration from the 'CourseDto' DTO back to the 'Course' entity model.
            // This mapping will be used when converting a 'CourseDto' instance back to a 'Course' instance.
            // Useful for updating and creating operations where the input comes from client-side DTOs.
            CreateMap<CourseDto, Course>();

            CreateMap<Permission, UserDto>();
            CreateMap<UserDto, Permission>();
        }
    }
}
