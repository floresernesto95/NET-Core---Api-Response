using FluentValidation;
using Project_Courses.Dtos;

namespace Project_Courses.Validators
{
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            // Name is required, must be at least 10 characters long and a maximum of 100 characters.
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(10, 100).WithMessage("Name must be between 10 and 100 characters.");

            // Description is optional, but if provided, must not exceed 1000 characters.
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description can't exceed 1000 characters.");
        }
    }
}
