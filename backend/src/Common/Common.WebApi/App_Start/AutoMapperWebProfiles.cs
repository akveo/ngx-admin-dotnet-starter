/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.Entities;

namespace Common.WebApi
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<ApplicationUser, User>().ReverseMap();
            CreateMap<ApplicationRole, Role>().ReverseMap();
            CreateMap<ApplicationUserRole, UserRole>().ReverseMap();
            CreateMap<ApplicationUserClaim, UserClaim>().ReverseMap();
        }
    }
}