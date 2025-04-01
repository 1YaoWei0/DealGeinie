using DealGeinieCrmService.Utilities;
using System.Web.Http;
using System.Web.Http.Description;

namespace DealGeinieCrmService.Controllers
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        //[HttpPost]
        //[Route("login")]
        //[AllowAnonymous]
        //[ResponseType(typeof(string))]
        //public IHttpActionResult Login(LoginRequest request)
        //{
        //    // 示例验证逻辑（实际需替换为数据库验证）
        //    if (request.Username == "admin" && request.Password == "admin123")
        //    {
        //        var token = JwtManager.GenerateToken(request.Username, "Admin");
        //        return Ok(new { Token = token });
        //    }

        //    return Unauthorized(); // 401 未授权
        //}
    }
}
