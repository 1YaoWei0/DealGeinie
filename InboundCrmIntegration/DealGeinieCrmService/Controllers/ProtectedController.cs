using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DealGeinieCrmService.Controllers
{
    [RoutePrefix("api/protected")]
    [Authorize] // 整个控制器需要认证
    public class ProtectedController : ApiController
    {
        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin")] // 仅允许 Admin 角色访问
        public IHttpActionResult AdminOnly()
        {
            var username = User.Identity.Name;
            return Ok($"Hello Admin {username}!");
        }

        [HttpGet]
        [Route("user")]
        public IHttpActionResult AnyAuthenticatedUser()
        {
            return Ok($"Welcome {User.Identity.Name}!");
        }
    }
}
