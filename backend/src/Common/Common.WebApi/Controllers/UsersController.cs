/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the �docs� folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Services.Infrastructure;
using Common.WebApi.Identity;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Entities;

namespace Common.WebApi.Controllers
{
    [RoutePrefix("users")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : BaseApiController
    {
        protected readonly IUserService userService;
        protected readonly IAuthenticationService authService;

        public UsersController(IUserService userService, IAuthenticationService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await userService.GetById(id);
            return Ok(user);
        }

        [HttpGet]
        [Route("current")]
        [OverrideAuthorization]
        [Authorize]
        public async Task<IHttpActionResult> GetCurrent()
        {
            int.TryParse(User.Identity.GetUserId(), out var currentUserId);
            if (currentUserId > 0)
            {
                var user = await userService.GetById(currentUserId);
                return Ok(user);
            }

            return Unauthorized();
        }

        [HttpPut]
        [Route("current")]
        [OverrideAuthorization]
        [Authorize]
        public async Task<IHttpActionResult> EditCurrent(UserDTO userDto)
        {
            int.TryParse(User.Identity.GetUserId(), out var currentUserId);
            if (currentUserId != userDto.Id)
            {
                return BadRequest();
            }

            await userService.Edit(userDto);

            var newToken = await authService.GenerateToken(currentUserId);

            return Ok(newToken);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(UserDTO userDto)
        {
            if (userDto.Id != 0)
            {
                return BadRequest();
            }

            var result = await userService.Edit(userDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Edit(int id, UserDTO userDto)
        {
            if (id != userDto.Id)
                return BadRequest();

            var result = await userService.Edit(userDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await userService.Delete(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("{userId:int}/photo")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> UserPhoto(int userId, string token)
        {
            var user = JwtManager.GetPrincipal(token);
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            var photoContent = await userService.GetUserPhoto(userId);

            if (photoContent == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            using (var ms = new MemoryStream(photoContent))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ms.ToArray())
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return response;
            }
        }
    }
}