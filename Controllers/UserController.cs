using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Courses.Dtos;
using Project_Courses.Services.User;
using System.Net;

namespace Project_Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "edit,view,create")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUser()
        {
            var user = await _userService.GetUserAsync();

            if (user == null)
            {
                return NotFound(new ApiResponse<UserDto>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = "User not found."
                });
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(new ApiResponse<UserDto>
            {
                Status = HttpStatusCode.OK,
                Data = userDto
            });
        }
    }
}
