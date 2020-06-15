/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.WebApi.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Common.WebApi.Controllers
{
    [RoutePrefix("users/{id:int}/roles")]
    [Authorize(Roles = Roles.Admin)]
    public class RolesController : BaseApiController
    {
        protected readonly IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Assign(int id, RoleDTO role)
        {
            var result = await roleService.AssignToRole(id, role.Name);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Unassign(int id, RoleDTO role)
        {
            var result = await roleService.UnassignRole(id, role.Name);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetRoles(int id)
        {
            var result = await roleService.GetRoles(id);
            return Ok(result);
        }
    }
}