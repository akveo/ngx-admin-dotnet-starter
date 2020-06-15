/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Microsoft.Owin;
using Microsoft.Owin.Cors;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Cors;

namespace Common.WebApi
{
    public class OwinCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly CorsPolicy _policy;

        public OwinCorsPolicyProvider()
        {
            var settings = ConfigurationManager.AppSettings;

            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            if (!string.IsNullOrEmpty(settings["origin"]))
            {
                _policy.Origins.Add(settings["origin"]);
            }
            else
            {
                _policy.AllowAnyOrigin = true;
            }
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request)
        {
            return Task.FromResult(_policy);
        }
    }
}
