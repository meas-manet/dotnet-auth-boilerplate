using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_auth_boilerplate.Dtos.UserProfile;

namespace dotnet_auth_boilerplate
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, GetUserProfileDto>();
            CreateMap<AddUserProfileDto, UserProfile>();
        }
    }
}