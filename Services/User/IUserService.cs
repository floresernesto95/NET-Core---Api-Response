using Project_Courses.Models;

namespace Project_Courses.Services.User
{
    public interface IUserService
    {
        Task<Permission> GetUserAsync();
    }
}