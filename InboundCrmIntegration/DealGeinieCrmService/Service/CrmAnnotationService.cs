using DealGeinieCrmService.Authentications;
using DealGeinieCrmService.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Net;

namespace DealGeinieCrmService.Services
{
    public abstract class CrmAnnotationService : ICrmService
    {
        protected ICrmCredential crmCredential;
        protected ICrmEntity crmEntity;
        protected IOrganizationService organizationServiceProxy;

        public CrmAnnotationService() { }

        public bool Create(string id, string entityName, string entityFieldName, string entityFieldValue)
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

            Guid accountid = FindEntityIdByName(organizationServiceProxy, id, entityName, entityFieldName, entityFieldValue);

            if (accountid == Guid.Empty) {
                return false;
            }

            return CreateAnnotation(accountid);
        }

        // process credential
        public void ProcessCredential(ICrmCredential _credential)
        {
            this.crmCredential = _credential;
        }

        public abstract bool GetOriganizationServiceProxy();

        public abstract bool CreateAnnotation(Guid _guid);

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

        public void ProcessEntity(ICrmEntity _entity)
        {
            this.crmEntity = _entity;
        }
    }
}
