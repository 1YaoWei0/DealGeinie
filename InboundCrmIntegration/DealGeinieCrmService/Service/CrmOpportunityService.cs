using DealGeinieCrmService.Authentications;
using DealGeinieCrmService.Models;
using DealGeinieCrmService.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.Web.Mvc;

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
        public Guid FindEntityIdByName(
            IOrganizationService service, 
            string id, 
            string entityName, 
            string entityFieldName, 
            string entityFieldValue)
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
        
        /// <summary>
        /// Willie Yao - 04/01/2025
        /// Find opportunity entity element.
        /// </summary>
        /// <returns>Opportunity entity element.</returns>
        public CrmOpportunityEntity FindOpportunityEntity(IOrganizationService service, string name)
        {            
            using (var context = new OrganizationServiceContext(service))
            {               
                var query = from a in context.CreateQuery("opportunity")
                            where (string)a["name"] == name
                            select a;

                foreach (var account in query)
                {
                    var a1 = account.GetAttributeValue<string>("name");
                    var a2 = account.GetAttributeValue<EntityReference>("parentaccountid");                    
                }
            }
            return new CrmOpportunityEntity();
        }  
        
        public Response UpdateOpportunityEntity(IOrganizationService service, CrmOpportunityEntity entity)
        {
            QueryExpression queryExpression = new QueryExpression("opportunity");
            queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, entity.Name);
            queryExpression.ColumnSet = new ColumnSet(new string[]
            {
                "hms_systemconstruction",
                "hms_projectbudget",
                "hms_approveproject",
                "hms_communication",
                "hms_note"
            });
            OptionSetValue optionSetValue = new OptionSetValue();
            try
            {
                EntityCollection result = service.RetrieveMultiple(queryExpression);
                if (result.Entities.Count > 0)
                {
                    Entity opportunity = result.Entities[0];
                    opportunity["hms_projectbudget"] = new OptionSetValue((int)entity.ProjectBudget);
                    opportunity["hms_approveproject"] = new OptionSetValue((int)entity.ApproveProject);
                    opportunity["hms_communication"] = new OptionSetValue((int)entity.CommunicationType);
                    opportunity["hms_note"] = entity.Note;
                    opportunity["hms_systemconstruction"] = entity.SystemConstruction;
                    service.Update(opportunity);

                    return new Response()
                    {
                        HttpStatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Update successfully!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    HttpStatusCode = System.Net.HttpStatusCode.ExpectationFailed,
                    Message = "Update failed!"
                };
            }            
            
            return new Response()
            {
                HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                Message = "Update failed!"
            };

        }
    }
}