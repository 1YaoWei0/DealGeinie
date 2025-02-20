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

        public void AddNoteToOpportunity(IOrganizationService service, Guid opportunityId, string noteText, string title = "")
        {
            Entity note = new Entity("annotation");

            // 设置注释的内容
            note["annotationid"] = noteText;

            // 设置关联的商机ID
            note["objectid"] = new EntityReference("opportunity", opportunityId);

            note["subject"] = title;

            // 创建注释
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

            QueryExpression query = new QueryExpression("opportunity");
            query.ColumnSet.AddColumns("name", "opportunityid");
            query.Criteria.AddCondition("name", ConditionOperator.Equal, "测试");


            // 执行查询
            EntityCollection result = organizationServiceProxy.RetrieveMultiple(query);

            // 检查结果并获取ID
            if (result.Entities.Count > 0)
            {
                Guid opportunityId = result.Entities[0].Id;

                AddNoteToOpportunity(organizationServiceProxy, opportunityId, "TEST CONTENT", "TEST TITLE");

            }
            else
            {
                Console.WriteLine("未找到商机");
            }
        }
    }
}
