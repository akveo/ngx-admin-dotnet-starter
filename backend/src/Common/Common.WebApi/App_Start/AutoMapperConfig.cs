/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using AutoMapper.Configuration;
using Common.Services.Infrastructure.MappingProfiles;

namespace Common.WebApi
{
    public static class AutoMapperConfig
    {
        public static void Configure(MapperConfigurationExpression config)
        {
            config.AllowNullCollections = false;

            config.AddProfile<IdentityUserProfile>();
            config.AddProfile<UserProfile>();
            config.AddProfile<SettingsProfile>();
        }
    }
}