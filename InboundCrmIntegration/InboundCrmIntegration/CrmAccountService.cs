using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace InboundCrmIntegration
{
    public class CrmAccountService
    {
        ICrmCredential crmCredential;
        ICrmEntity crmEntity;

        public CrmAccountService(ICrmEntity _crmEntity, ICrmCredential _crmCredential)
        {
            crmEntity = _crmEntity;
            crmCredential = _crmCredential;
        }

        public bool UpdateAccount()
        {
            CrmAccount crmAccount = (CrmAccount) crmEntity;
            TestAccountCredential testAccountCredential = (TestAccountCredential) crmCredential;

            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            IOrganizationService organizationServiceProxy = new OrganizationServiceProxy(new Uri(testAccountCredential.Uri), null, testAccountCredential.Credentials(), null);

            Entity entityCreate = new Entity("account"); 
            entityCreate["name"] = crmAccount.name;
            entityCreate["telephone1"] = crmAccount.telephone1; 
            //entityCreate["createBy"] = "Mina Xu"; 
            entityCreate["createdon"] = crmAccount.createdon;

            try
            {
                Guid entityId = organizationServiceProxy.Create(entityCreate);
            }
            catch (Exception ex)
            {                
                return false;
            }            

            return true;
        }

        private static bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
