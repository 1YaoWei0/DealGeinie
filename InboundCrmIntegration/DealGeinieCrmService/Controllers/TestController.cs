using DealGeinieCrmService.Models;
using System.Net;
using System.Web.Http;

namespace DealGeinieCrmService.Controllers
{
    public class TestController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/test")]
        public IHttpActionResult GetData()
        {
            var data = new { Id = 1, Name = "API authentication successful!" };
            return Ok(new ApiResponse<object>(HttpStatusCode.OK, data: data));
        }
    }
}
