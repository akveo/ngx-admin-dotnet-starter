/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using Microsoft.AspNet.Identity;
using System.Web;

namespace Common.WebApi
{
    public class CurrentContextProvider : ICurrentContextProvider
    {
        public ContextSession GetCurrentContext()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId<int>();
            return new ContextSession { UserId = currentUserId };
        }
    }
}
