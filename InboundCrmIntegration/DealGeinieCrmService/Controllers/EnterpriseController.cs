using DealGeinieCrmService.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Http;

namespace DealGeinieCrmService.Controllers
{
    [RoutePrefix("api/enterprise")]
    public class EnterpriseController : ApiController
    {
        private EnterpriseContext db = new EnterpriseContext();

        [Route("{id}")]
        [HttpGet]
        public IQueryable<EnterpriseInfo> GetEnterprises()
        {
            return db.Enterprises;
        }

        [Route("{id}")]
        [HttpPost]
        [ResponseType(typeof(EnterpriseInfo))]
        public async Task<IHttpActionResult> PostEnterprise(EnterpriseInfo enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enterprises.Add(enterprise);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi",
                new { id = enterprise.UnifiedSocialCreditCode }, enterprise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}