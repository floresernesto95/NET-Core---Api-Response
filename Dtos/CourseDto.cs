using System.ComponentModel.DataAnnotations;

namespace Project_Courses.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid CourseTypeId { get; set; } // This assumes you will handle the CourseType entity separately

        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
    }
}
