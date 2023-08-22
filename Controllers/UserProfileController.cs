using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_auth_boilerplate.Dtos.UserProfile;
using dotnet_auth_boilerplate.Services;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_auth_boilerplate.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserProfileDto>>> GetUserProfile(Guid id)
        {
            return Ok(await _userProfileService.GetUserProfile(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetUserProfileDto>>>> AddUserProfile(AddUserProfileDto addUserProfile)
        {
            return Ok(await _userProfileService.AddUserProfile(addUserProfile));
        }
    }
}