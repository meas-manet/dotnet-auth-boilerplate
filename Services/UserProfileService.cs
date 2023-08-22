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

        public Task<ServiceResponse<GetUserProfileDto>> UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetUserProfileDto>>> DeleteUserProfile(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}