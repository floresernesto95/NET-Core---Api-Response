namespace Project_Courses.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Module { get; set; } = null!;

        public string Feature { get; set; } = null!;

        public string? Description { get; set; }

        public bool Enabled { get; set; }
    }
}
