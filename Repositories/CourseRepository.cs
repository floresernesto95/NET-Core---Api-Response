using Microsoft.EntityFrameworkCore;
using Project_Courses.Models;

namespace Project_Courses.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        // DBXXXCONTEXT INJECTION
        // Private field to hold the database context.
        private readonly DbAa579fCoursesContext _context;

        // Constructor for the CourseRepository class. It takes an instance of the database context
        // as a parameter and assigns it to the private field. This context is injected by the dependency
        // injection system in ASP.NET Core, ensuring that the same context instance is used throughout
        // the request lifecycle (assuming it is configured as scoped).
        public CourseRepository(DbAa579fCoursesContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(Guid id)
        {
            // Use FirstOrDefaultAsync to retrieve a course by its ID.
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            // Mark the entity as modified.
            _context.Courses.Update(course);

            // Save changes to the database.
            await _context.SaveChangesAsync();

            // Return the updated entity.
            return course;
        }
    }
}
