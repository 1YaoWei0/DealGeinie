using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace InboundCrmIntegration.Services
{
    public class CrmAnnotationService
    {
        ICrmCredential crmCredential;

        public CrmAnnotationService(ICrmCredential _crmCredential)
        {
            crmCredential = _crmCredential;
        }

        public void AddNoteToOpportunity(IOrganizationService service, Guid accountid, string noteText, string title = "")
        {
            Entity note = new Entity("annotation");

            //// 设置注释的内容
            //note["annotationid"] = noteText;
            //note["isdocument"] = false;
            //// 设置关联的商机ID
            //note["subject"] = title;
            //note["objectid"] = new EntityReference("account", new Guid("e9161bbf-5ae8-ef11-811f-00155d01060d"));            
            // 创建注释
            
            string entitytype = "account";
            Entity Note = new Entity("annotation");
            Guid EntityToAttachTo = Guid.Parse("e9161bbf-5ae8-ef11-811f-00155d01060d"); // The GUID of the incident
            Note["objectid"] = new Microsoft.Xrm.Sdk.EntityReference(entitytype, EntityToAttachTo);
            Note["objecttypecode"] = entitytype;
            Note["subject"] = "Test Subject";
            Note["notetext"] = "Test note text";
            service.Create(Note);
            Guid noteId = service.Create(note);

            Console.WriteLine("注释已创建，ID: " + noteId.ToString());
        }

        private static bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public void run()
        {
            // 创建一个新的服务
            TestAccountCredential testAccountCredential = (TestAccountCredential) crmCredential;

            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            IOrganizationService organizationServiceProxy = new OrganizationServiceProxy(new Uri(testAccountCredential.Uri), null, testAccountCredential.Credentials(), null);

            QueryExpression query = new QueryExpression("account");
            query.ColumnSet.AddColumns("name", "accountid");
            query.Criteria.AddCondition("name", ConditionOperator.Equal, "尧威");


            // 执行查询
            EntityCollection result = organizationServiceProxy.RetrieveMultiple(query);

            // 检查结果并获取ID
            if (result.Entities.Count > 0)
            {
                Guid accountid = result.Entities[0].Id;

                AddNoteToOpportunity(organizationServiceProxy, accountid, "TEST CONTENT", "TEST TITLE");

            }
            else
            {
                Console.WriteLine("未找到商机");
            }
        }
    }
}
