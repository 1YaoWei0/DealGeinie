using DealGeinieCrmService.Authentications;
using DealGeinieCrmService.Models;
using DealGeinieCrmService.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DealGeinieCrmService.Service
{
    public class CrmOpportunityService : ICrmService
    {
        protected ICrmCredential crmCredential;
        protected ICrmEntity crmEntity;
        protected IOrganizationService organizationServiceProxy;

        public void ProcessEntity(ICrmEntity _entity)
        {
            crmEntity = _entity;
        }

        public bool Create()
        {
            if (!ExistEntity())
            {
                return false;
            }
            if (!ExistCredential())
            {
                return false;
            }
            if (!GetOriganizationServiceProxy() && organizationServiceProxy == null)
            {
                return false;
            }

            return CreateOpportunity();
        }

        private bool CreateOpportunity()
        {
            CrmTxtAnnotationService crmTxtAnnotationService = new CrmTxtAnnotationService();
            crmTxtAnnotationService.ProcessEntity(crmEntity);
            crmTxtAnnotationService.ProcessCredential(crmCredential);

            bool result = crmTxtAnnotationService.Create("id", "entityName", "entityFieldName", "entityFieldValue");

            return result;
        }

        public bool GetOriganizationServiceProxy()
        {
            organizationServiceProxy = ((CrmDevCredential) crmCredential).OrganizationServiceProxy;
            if (organizationServiceProxy == null)
            {
                return false;
            }
            return true;
        }

        // Find the entity id by name and return the entity id
        public Guid FindEntityIdByName(IOrganizationService service, string id, string entityName, string entityFieldName, string entityFieldValue)
        {
            QueryExpression query = new QueryExpression(entityName);
            query.ColumnSet.AddColumns("name", id);
            query.Criteria.AddCondition(entityFieldName, ConditionOperator.Equal, entityFieldValue);

            EntityCollection result = service.RetrieveMultiple(query);

            if (result.Entities.Count > 0)
            {
                return result.Entities[0].Id;
            }
            else
            {
                Console.WriteLine("Not Found");
            }

            return Guid.Empty;
        }

        private bool ExistEntity()
        {
            if (crmEntity == null)
            {
                return false;
            }

            return true;
        }

        private bool ExistCredential()
        {
            if (crmCredential == null)
            {
                return false;
            }

            return true;
        }
    }
}