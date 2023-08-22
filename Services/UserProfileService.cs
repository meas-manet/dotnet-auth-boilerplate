using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_auth_boilerplate.Dtos.UserProfile;

namespace dotnet_auth_boilerplate.Services
{
    public class UserProfileService : IUserProfileService
    {
        Task<ServiceResponse<GetUserProfileDto>> IUserProfileService.GetUserProfile(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<List<GetUserProfileDto>>> IUserProfileService.AddUserProfile(AddUserProfileDto addUserProfile)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<GetUserProfileDto>> IUserProfileService.UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<List<GetUserProfileDto>>> IUserProfileService.DeleteUserProfile(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}