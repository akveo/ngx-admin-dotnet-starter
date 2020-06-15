/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.IdentityManagement;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Common.WebApi.Identity
{
    public class AuthenticationService<TUser> : IAuthenticationService
        where TUser : Entities.User, IUser<int>, new()
    {
        protected readonly UserManager<TUser, int> userManager;
        public AuthenticationService(UserManager<TUser, int> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<AuthResult<Token>> Login(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                return AuthResult<Token>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && user.Id > 0 && !user.IsDeleted)
            {
                if (await userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    var token = JwtManager.GenerateToken(await userManager.CreateIdentityAsync(user));
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        public async Task<AuthResult<Token>> ChangePassword(ChangePasswordDTO changePasswordDto)
        {
            if (changePasswordDto == null ||
                string.IsNullOrEmpty(changePasswordDto.ConfirmPassword) ||
                string.IsNullOrEmpty(changePasswordDto.Password) ||
                changePasswordDto.Password != changePasswordDto.ConfirmPassword
            )
                return AuthResult<Token>.UnvalidatedResult;

            int.TryParse(HttpContext.Current.User.Identity.GetUserId(), out var currentUserId);
            if (currentUserId > 0)
            {
                var result = await userManager.ChangePasswordAsync(currentUserId, null, changePasswordDto.Password);
                if (result.Succeeded)
                    return AuthResult<Token>.SucceededResult;
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        public async Task<AuthResult<Token>> SignUp(SignUpDTO signUpDto)
        {
            if (signUpDto == null ||
                string.IsNullOrEmpty(signUpDto.Email) ||
                string.IsNullOrEmpty(signUpDto.Password) ||
                string.IsNullOrEmpty(signUpDto.ConfirmPassword) ||
                string.IsNullOrEmpty(signUpDto.FullName) ||
                signUpDto.Password != signUpDto.ConfirmPassword
            )
                return AuthResult<Token>.UnvalidatedResult;

            var newUser = new TUser { Login = signUpDto.FullName, Email = signUpDto.Email };

            var result = await userManager.CreateAsync(newUser, signUpDto.Password);

            if (result.Succeeded)
            {
                if (newUser.Id > 0)
                {
                    await userManager.AddToRoleAsync(newUser.Id, "User");
                    var token = JwtManager.GenerateToken(await userManager.CreateIdentityAsync(newUser));
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnauthorizedResult;
        }

        public async Task<AuthResult<string>> RequestPassword(RequestPasswordDTO requestPasswordDto)
        {
            if (requestPasswordDto == null ||
                string.IsNullOrEmpty(requestPasswordDto.Email))
                return AuthResult<string>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(requestPasswordDto.Email);

            if (user != null && user.Id > 0 && !user.IsDeleted)
            {
                var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                return AuthResult<string>.TokenResult(passwordResetToken);
            }

            return AuthResult<string>.UnvalidatedResult;
        }

        public async Task<AuthResult<Token>> RestorePassword(RestorePasswordDTO restorePasswordDto)
        {
            if (restorePasswordDto == null ||
                string.IsNullOrEmpty(restorePasswordDto.Email) ||
                string.IsNullOrEmpty(restorePasswordDto.Token) ||
                string.IsNullOrEmpty(restorePasswordDto.NewPassword) ||
                string.IsNullOrEmpty(restorePasswordDto.ConfirmPassword) ||
                string.IsNullOrEmpty(restorePasswordDto.ConfirmPassword) ||
                restorePasswordDto.ConfirmPassword != restorePasswordDto.NewPassword
            )
                return AuthResult<Token>.UnvalidatedResult;

            var user = await userManager.FindByEmailAsync(restorePasswordDto.Email);

            if (user != null && user.Id > 0 && !user.IsDeleted)
            {
                var result = await userManager.ResetPasswordAsync(user.Id, restorePasswordDto.Token, restorePasswordDto.NewPassword);

                if (result.Succeeded)
                {
                    var token = JwtManager.GenerateToken(await userManager.CreateIdentityAsync(user));
                    return AuthResult<Token>.TokenResult(token);
                }
            }

            return AuthResult<Token>.UnvalidatedResult;
        }

        public Task<AuthResult<Token>> SignOut()
        {
            throw new System.NotImplementedException();
        }

        public async Task<AuthResult<Token>> RefreshToken(RefreshTokenDTO refreshTokenDto)
        {
            var refreshToken = refreshTokenDto?.Token?.Refresh_token;
            if (string.IsNullOrEmpty(refreshToken))
                return AuthResult<Token>.UnvalidatedResult;

            try
            {
                var principal = JwtManager.GetPrincipal(refreshToken, isAccessToken: false);
                int.TryParse(principal.Identity.GetUserId(), out var currentUserId);

                var user = await userManager.FindByIdAsync(currentUserId);

                if (user != null && user.Id > 0 && !user.IsDeleted)
                {
                    var token = JwtManager.GenerateToken(await userManager.CreateIdentityAsync(user));
                    return AuthResult<Token>.TokenResult(token);
                }
            }
            catch (Exception)
            {
                return AuthResult<Token>.UnauthorizedResult;
            }

            return AuthResult<Token>.UnauthorizedResult;
        }
        
        public async Task<Token> GenerateToken(int userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null && user.Id > 0)
            {
                return JwtManager.GenerateToken(await userManager.CreateIdentityAsync(user));
            }

            return null;
        }
    }
}