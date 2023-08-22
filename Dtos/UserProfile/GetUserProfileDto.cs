using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_auth_boilerplate.Dtos.User;

namespace dotnet_auth_boilerplate.Dtos.UserProfile
{
    public class GetUserProfileDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}