using Project_Courses.Models;

namespace Project_Courses.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourses();
        Task<Course> GetCourseById(Guid id);
        Task<Course> UpdateCourse(Course course);
    }
}
