using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Courses.Dtos;
using Project_Courses.Services.Auth;
using System.Net;

namespace Project_Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _permissionService;
        
        public LoginController(ILoginService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginDto loginDto)
        {
            var token = await _permissionService.ValidatePermissionAsync(loginDto.Id);

            if (token == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = "No token found for the provided ID."
                });
            }

            return Ok(new ApiResponse<string>
            {
                Status = HttpStatusCode.OK,
                Data = token
            });
        }
    }
}
