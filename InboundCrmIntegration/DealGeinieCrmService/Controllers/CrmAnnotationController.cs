using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using DealGeinieCrmService.Models;
using DealGeinieCrmService.Services;

namespace DealGeinieCrmService.Controllers
{
    [RoutePrefix("api/annotations")]
    public class CrmAnnotationController : ApiController
    {
        private readonly ICrmService CrmService;

        public CrmAnnotationController(ICrmService _crmService)
        {
            CrmService = _crmService;
        }

        [Route("{id}")]
        [HttpPost]
        public HttpResponseMessage CreateTxtAnnotation(CrmTxtAnnotationEntity _crmTxtAnnotationEntity)
        {
            var crmTxtAnnotationService = (CrmTxtAnnotationService) CrmService;

            
            //TestAccountCredential testAccountCredential = new TestAccountCredential();
            //var crmAnnotationService = new CrmAnnotationService(testAccountCredential);
            //crmAnnotationService.run();

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}