using InboundCrmIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using InboundCrmIntegration.Services;
using DealGeinieCrmService.Models;

namespace DealGeinieCrmService.Controllers
{
    public class CrmAnnotationController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetCrmAnnotationToOpportunity()
        {
            TestAccountCredential testAccountCredential = new TestAccountCredential();
            var crmAnnotationService = new CrmAnnotationService(testAccountCredential);
            crmAnnotationService.run();

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}