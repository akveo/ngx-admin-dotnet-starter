/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;

[assembly: OwinStartup(typeof(Common.WebApi.Startup))]
namespace Common.WebApi
{
    public class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public virtual void Configuration(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            ConfigureJwtAuth(app);

            app.UseCors(new Microsoft.Owin.Cors.CorsOptions
            {
                PolicyProvider = new OwinCorsPolicyProvider()
            });

            FilterConfig.RegisterFilters(GlobalConfiguration.Configuration.Filters);
            RouteConfig.ConfigureRoutes(GlobalConfiguration.Configuration);
            WebApiConfig.Configure(GlobalConfiguration.Configuration);
            SwaggerConfig.Configure(GlobalConfiguration.Configuration);
            RegisterDependencies();
            ConfigureMapping();

            GlobalConfiguration.Configuration.EnsureInitialized();

            app.UseWebApi(GlobalConfiguration.Configuration);

            SerilogConfig.Configure();
        }

        private void RegisterDependencies()
        {
            var container = new UnityContainer();
            UnityConfig.RegisterDependencies(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private void ConfigureMapping()
        {
            var config = new AutoMapper.Configuration.MapperConfigurationExpression();

            AutoMapperConfig.Configure(config);

            AutoMapper.Mapper.Initialize(config);
        }

        public void ConfigureJwtAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["issuer"];
            var audience = ConfigurationManager.AppSettings["audience"];
            var accessSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["accessSecret"]);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(accessSecret),
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true
                },
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audience },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(issuer, accessSecret)
                    }
            });
        }
    }
}