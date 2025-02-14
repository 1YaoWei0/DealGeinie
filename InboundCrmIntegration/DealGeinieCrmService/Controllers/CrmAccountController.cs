using DealGeinieCrmService.Models;
using InboundCrmIntegration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DealGeinieCrmService.Controllers
{
    public class CrmAccountController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PostCrmAccount(Models.CrmAccount _crmAccount)
        {
            TestAccountCredential testAccountCredential = new TestAccountCredential();
            var crmAccount = new InboundCrmIntegration.CrmAccount(_crmAccount.name, _crmAccount.telephone1, _crmAccount.createdon);
            CrmAccountService crmAccountService = new CrmAccountService(crmAccount, testAccountCredential);
            bool result = crmAccountService.UpdateAccount();            

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.Created, _crmAccount);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
