using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_auth_boilerplate.Dtos.UserProfile;

namespace dotnet_auth_boilerplate.Services
{
    public interface IUserProfileService
    {
        Task<ServiceResponse<GetUserProfileDto>> GetUserProfile(Guid id);
        Task<ServiceResponse<List<GetUserProfileDto>>> AddUserProfile(AddUserProfileDto addUserProfile);
        Task<ServiceResponse<GetUserProfileDto>> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto);
        Task<ServiceResponse<List<GetUserProfileDto>>> DeleteUserProfile(Guid id);
    }
}