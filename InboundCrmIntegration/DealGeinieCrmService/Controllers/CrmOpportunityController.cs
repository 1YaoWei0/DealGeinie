using System;
using System.Net;
using System.Web.Http;
using DealGeinieCrmService.Models;
using DealGeinieCrmService.Service;
using DealGeinieCrmService.Authentications;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;

namespace DealGeinieCrmService.Controllers
{
    [Authorize]
    [RoutePrefix("api/opportunities")]
    public class CrmOpportunityController : ApiController
    {
        [Route("{id}")]
        [HttpPost]
        public IHttpActionResult UpdateOpportunityData([FromBody] CrmOpportunityEntity _crmOpportunityEntity)
        {
            CrmOpportunityService crmOpportunityService = new CrmOpportunityService();
            TestAccountCredential testAccountCredential = new TestAccountCredential();
            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IOrganizationService organizationServiceProxy = new OrganizationServiceProxy(new Uri(testAccountCredential.Uri), null, testAccountCredential.Credentials(), null);

            return Ok(crmOpportunityService.UpdateOpportunityEntity(organizationServiceProxy, _crmOpportunityEntity));
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult QueryOpportunityData(string name)
        {
            CrmOpportunityService crmOpportunityService = new CrmOpportunityService();
            TestAccountCredential testAccountCredential = new TestAccountCredential();
            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IOrganizationService organizationServiceProxy = new OrganizationServiceProxy(new Uri(testAccountCredential.Uri), null, testAccountCredential.Credentials(), null);

            return Ok(crmOpportunityService.FindOpportunityEntity(organizationServiceProxy, name));
        }

        private static bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}