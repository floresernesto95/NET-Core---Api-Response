using Project_Courses.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace Project_Courses.Services.User
{
    public class UserService : IUserService
    {
        private readonly DbAa579fCoursesContext _context;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public UserService(DbAa579fCoursesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccesor = httpContextAccessor;
        }

        public async Task<Permission> GetUserAsync()
        {
            if (_httpContextAccesor.HttpContext.User == null)
            {
               return null;
            }

            var userIdClaim = _httpContextAccesor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var userId = Guid.Parse(userIdClaim.Value);
            var user = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == userId);

            return user;
        }
    }
}
