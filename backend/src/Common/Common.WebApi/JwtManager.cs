/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.DTO;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.WebApi
{
    public static class JwtManager
    {
        private static readonly byte[] AccessSecret;
        private static readonly byte[] RefreshSecret;
        private static readonly string Issuer;
        private static readonly string Audience;
        private static readonly int AccessExpirationTime;
        private static readonly int RefreshExpirationTime;

        static JwtManager()
        {
            Issuer = ConfigurationManager.AppSettings["issuer"];
            Audience = ConfigurationManager.AppSettings["audience"];
            AccessSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["accessSecret"]);
            RefreshSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["refreshSecret"]);
            AccessExpirationTime = Int32.Parse(ConfigurationManager.AppSettings["accessExpire"]);
            RefreshExpirationTime = Int32.Parse(ConfigurationManager.AppSettings["refreshExpire"]);
        }

        public static Token GenerateToken(ClaimsIdentity claims)
        {
            var issued = DateTime.UtcNow;
            var accessExpires = issued.AddMinutes(AccessExpirationTime);
            var refreshExpires = issued.AddMinutes(RefreshExpirationTime);

            var token = new Token
            {
                Expires_in = TimeSpan.FromMinutes(AccessExpirationTime).TotalMilliseconds,
                Access_token = CreateToken(claims, issued, accessExpires, AccessSecret),
                Refresh_token = CreateToken(claims, issued, refreshExpires, RefreshSecret)
            };

            return token;
        }

        private static string CreateToken(ClaimsIdentity claims, DateTime issuedAt, DateTime expireIn, byte[] secretKey)
        {
            var signingKey = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

            var roleClaim = claims.FindFirst(claims.RoleClaimType);
            if (roleClaim != null)
            {
                claims.AddClaim(new Claim("role", roleClaim.Value, roleClaim.ValueType, roleClaim.Issuer, roleClaim.OriginalIssuer, roleClaim.Subject));
                claims.TryRemoveClaim(roleClaim);
            }

            var token = new JwtSecurityToken(Issuer, Audience, claims.Claims, issuedAt, expireIn, signingKey);
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public static ClaimsPrincipal GetPrincipal(string token, bool isAccessToken = true)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                    return null;

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidIssuer = Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(isAccessToken ? AccessSecret : RefreshSecret)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}