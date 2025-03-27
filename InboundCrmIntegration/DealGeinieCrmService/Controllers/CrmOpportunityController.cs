using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using DealGeinieCrmService.Models;
using DealGeinieCrmService.Services;
using System.Threading.Tasks;
using DealGeinieCrmService.Service;

namespace DealGeinieCrmService.Controllers
{
    [RoutePrefix("api/opportunities")]
    public class CrmOpportunityController : ApiController
    {
        [Route("{id}")]
        [HttpPost]
        public IHttpActionResult SyncOpportunityData(CrmOpportunityEntity _crmOpportunityEntity)
        {
            CrmOpportunityService crmOpportunityService = new CrmOpportunityService();

            crmOpportunityService.ProcessEntity(_crmOpportunityEntity);
            crmOpportunityService.Create();

            return Ok();
        }
    }
}