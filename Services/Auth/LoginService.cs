
using Project_Courses.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Project_Courses.Services.Auth
{
    public class LoginService : ILoginService
    {
        private readonly DbAa579fCoursesContext _context;

        public LoginService(DbAa579fCoursesContext context)
        {
            _context = context;
        }

        public async Task<string> ValidatePermissionAsync(Guid id)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == id);

            if (permission == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, permission.Id.ToString()),
                new Claim(ClaimTypes.Name, permission.Module),
                new Claim(ClaimTypes.Role, permission.Feature),
            };

            var tokenDescription = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("If debugging is the process of removing software bugs, then programming must be the process of putting them in.\r\n")
                    ), 
                    SecurityAlgorithms.HmacSha256Signature)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);
            
            return token;
        }
    }
}
