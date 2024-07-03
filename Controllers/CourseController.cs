using Microsoft.AspNetCore.Mvc;
using Project_Courses.Models;
using Project_Courses.Repositories;
using AutoMapper;
using Project_Courses.Dtos; // Ensure AutoMapper is available for dependency injection
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Project_Courses.Controllers
{
    // Defines the route at the controller level, which means all actions within this controller will start with "api/Courses".
    [Route("api/[controller]")]
    // Indicates that this class is an API controller and automatically enables certain behaviors like model validation.
    [ApiController]
    public class CourseController : Controller
    {

        // XXXREPOSITORY INJECTION
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper; // Add IMapper dependency

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CourseDto>>>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllCourses();

            if (courses == null)
            {
                return NotFound(new ApiResponse<List<CourseDto>>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = "No courses found."
                });
            }

            var coursesDto = _mapper.Map<List<CourseDto>>(courses);

            return Ok(new ApiResponse<List<CourseDto>>
            {
                Status = HttpStatusCode.OK,
                Data = coursesDto
            });
        }


        // GET: api/Courses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CourseDto>>> GetCourseById(Guid id)
        {
            var course = await _courseRepository.GetCourseById(id);

            if (course == null)
            {
                return NotFound(new ApiResponse<CourseDto>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = "Course not found."
                });
            }

            var courseDto = _mapper.Map<CourseDto>(course);

            return Ok(new ApiResponse<CourseDto>
            {
                Status = HttpStatusCode.OK,
                Data = courseDto
            });
        }


        // PUT: api/Courses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<CourseDto>>> UpdateCourse(Guid id, [FromBody] CourseDto courseDto)
        {
            // Check if the ID in the URL matches the ID in the provided course object. If not, return a 400 Bad Request.
            if (id != courseDto.Id)
            {
                return BadRequest(new ApiResponse<CourseDto>
                {
                    Status = HttpStatusCode.BadRequest,
                    ErrorMessage = "Course ID mismatch"
                });
            }

            // Check if the course exists before attempting to update it.
            var existingCourse = await _courseRepository.GetCourseById(id);
            
            if (existingCourse == null)
            {
                return NotFound(new ApiResponse<CourseDto>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = "Course not found"
                });
            }

            // Map the changes from DTO to the existing entity
            _mapper.Map(courseDto, existingCourse);

            // Update the course entity
            await _courseRepository.UpdateCourse(existingCourse);

            // Map updated entity back to DTO
            var updatedCourseDto = _mapper.Map<CourseDto>(existingCourse);

            // Return the updated course data wrapped in ApiResponse
            return Ok(new ApiResponse<CourseDto>
            {
                Status = HttpStatusCode.OK,
                Data = updatedCourseDto
            });
        }
    }
}
