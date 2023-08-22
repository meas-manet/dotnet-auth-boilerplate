using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_auth_boilerplate.Dtos.UserProfile;


namespace dotnet_auth_boilerplate.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private Guid GetUserId() => Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<GetUserProfileDto>> GetUserProfile(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetUserProfileDto>();
            var dbUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserProfileDto>(dbUserProfile);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserProfileDto>>> AddUserProfile(AddUserProfileDto addUserProfile)
        {
            var serviceResponse = new ServiceResponse<List<GetUserProfileDto>>();
            var userProfile = _mapper.Map<UserProfile>(addUserProfile);
            userProfile.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.UserProfiles
                .Where(u => u.User!.Id == GetUserId())
                .Select(u => _mapper.Map<GetUserProfileDto>(u))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserProfileDto>> UpdateUserProfile(UpdateUserProfileDto updatedUserProfile)
        {
            var serviceResponse = new ServiceResponse<GetUserProfileDto>();
            try
            {
                var userProfile = await _context.UserProfiles
                                        .Include(u => u.User)
                                        .FirstOrDefaultAsync(u => u.Id == updatedUserProfile.Id);
                if (userProfile is null || userProfile.User!.Id != GetUserId())
                {
                    throw new Exception($"User Profile not found");
                }

                userProfile.Firstname = updatedUserProfile.Firstname;
                userProfile.Lastname = updatedUserProfile.Lastname;
                userProfile.PhoneNumber = updatedUserProfile.PhoneNumber;
                userProfile.Email = updatedUserProfile.Email;
                userProfile.Address = updatedUserProfile.Address;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUserProfileDto>(userProfile);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserProfileDto>>> DeleteUserProfile(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserProfileDto>>();
            try
            {
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Id == id && u.User!.Id == GetUserId());
                if (userProfile is null)
                {
                    throw new Exception($"User Profile not found");
                }
                _context.UserProfiles.Remove(userProfile);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.UserProfiles.Where(u => u.User!.Id == GetUserId())
                                        .Select(u => _mapper.Map<GetUserProfileDto>(u)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}