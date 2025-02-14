using Microsoft.Xrm.Sdk;
using System;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Deployment;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.DirectoryServices.ActiveDirectory;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Web.Services.Description;

namespace InboundCrmIntegration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //string connectString = "Url=https://crmdev.huamei.com/XRMServices/2011/Organization.svc; Domain=huamei.com; Username=willie.Yao@huamei.com; Password=pass@word123";

            // 🔹 声明并初始化 clientCredentials
            ClientCredentials clientCredentials = new ClientCredentials();

            clientCredentials.UserName.UserName = "willie.Yao" + "@" + "huamei.com";
            clientCredentials.UserName.Password = "pass@word123";
            //clientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("willie.Yao", "pass@word123", "huamei.com");
            IOrganizationService organizationServiceProxy = new OrganizationServiceProxy(new Uri("https://crmdev.huamei.com/XRMServices/2011/Organization.svc"), null, clientCredentials, null);

            // var entitySingle = organizationServiceProxy.Retrieve("account", new Guid("e9161bbf-5ae8-ef11-811f-00155d01060d"), new ColumnSet("accountid"));
            // Console.WriteLine($"账户名称：{entitySingle["name"]}");

            // 一、下拉所有可用的实体
            /* RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest()
            //{
            //    EntityFilters = EntityFilters.Entity,
            //    RetrieveAsIfPublished = true
            //};
            //RetrieveAllEntitiesResponse response = (RetrieveAllEntitiesResponse)organizationServiceProxy.Execute(request);
 
            //foreach (var entityMetadata in response.EntityMetadata)
            //{
            //    Console.WriteLine($"Entity Logical Name: {entityMetadata.LogicalName}");
            }*/

            // 二、给存在的contact创建一条新记录
            Entity entityCreate = new Entity("account");  // 替换为你自定义实体的逻辑名称

            // 设置实体字段的值
            // entityCreate["name"] = "";  // 例如设置名称字段 该XXid字段不能被赋值 是由crm系统赋值
            entityCreate["name"] = "Mina Xu";  // 设置描述字段
            entityCreate["telephone1"] = "15399069623";  // 设置描述字段
            // 可以设置更多字段
            //entityCreate["createBy"] = "Mina Xu"; // 自定义日期字段
            entityCreate["createdon"] = DateTime.Now; // 自定义日期字段
            // 调用Create方法将记录保存到CRM中
            Guid entityId = organizationServiceProxy.Create(entityCreate);

            // 打印创建的记录ID
            Console.WriteLine($"新记录的ID是：{entityId}");
            //新记录的ID是：3cad7804-dae9-ef11-811f-00155d01060d



            //三、删除,物理删除
            /*
            organizationServiceProxy.Delete("account", new Guid("3cad7804-dae9-ef11-811f-00155d01060d"));
            */

            //四、更新, 在更新时建议使用这种方式, 如果将原有的数据全部查出来再修改部分字段, 会导致没有修改的字段也会被update, 可能会触发某些插件、流程
            /*
            var entityUpdate = new Entity("account", new Guid("3cad7804-dae9-ef11-811f-00155d01060d"));//必须填写被更新的id
            entityUpdate["telephone1"] = "15399069624";
            //...更新的赋值和新增一致, 不赘述
            organizationServiceProxy.Update(entityUpdate);
            */

            //五、查询单条: Retrieve 此方法没有找到数据会直接引发异常慎用(tableName With Id = xxx Does Not Exist), 类似.First()
            /*
            var entitySingle = organizationServiceProxy.Retrieve("account", new Guid("3cad7804-dae9-ef11-811f-00155d01060d"), new ColumnSet("name", "createdon"));
 
            // 判断字段是否存在并获取值
            if (entitySingle.Contains("name"))
            {
                Console.WriteLine($"账户名称：{entitySingle["name"]}");
            }
            else
            {
                Console.WriteLine("该记录没有 'name' 字段的值");
            }
 
            if (entitySingle.Contains("createdon"))
            {
                Console.WriteLine($"创建时间：{entitySingle["createdon"]}");
            }
            else
            {
                Console.WriteLine("该记录没有 'createdon' 字段的值");
            }
            */
        }
        private static bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
