using DealGeinieCrmService.Models;
using DealGeinieCrmService.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace DealGeinieCrmService.Service
{
    /// <summary>
    /// CRM opportunity service class
    /// Willie Yao - 04/02/2025
    /// </summary>
    public class CrmOpportunityService
    {
        

        /// <summary>
        /// Willie Yao - 04/01/2025
        /// Find opportunity entity element.
        /// </summary>
        /// <returns>Opportunity entity element.</returns>
        public CrmAnnotationEntityBase FindOpportunityEntity(IOrganizationService service, string name)
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
            return new CrmAnnotationEntityBase();
        }

        /// <summary>
        /// Update opportunity
        /// Willie Yao - 04/02/2025
        /// </summary>
        /// <param name="service">IOrganizationService</param>
        /// <param name="entity">CrmOpportunityEntity</param>
        /// <returns>Response</returns>
        public Response UpdateOpportunityEntity(IOrganizationService service, CrmOpportunityEntity entity)
        {
            QueryExpression queryExpression = new QueryExpression("opportunity");
            queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, entity.Name);
            queryExpression.ColumnSet = new ColumnSet(new string[]
            {
                "opportunityid",
                "hms_systemconstruction",
                "hms_projectbudget",
                "hms_approveproject",
                "hms_communication",
                "hms_note",
                "customerneed",
                "proposedsolution",
                "currentsituation"
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
                    opportunity["hms_note"] = entity.Note.IsNullOrWhiteSpace() ? opportunity.GetAttributeValue<string>("hms_note") : entity.Note;
                    opportunity["hms_systemconstruction"] = entity.SystemConstruction.IsNullOrWhiteSpace() ? opportunity.GetAttributeValue<string>("hms_systemconstruction") : entity.SystemConstruction;
                    opportunity["customerneed"] = entity.CustomerNeed.IsNullOrWhiteSpace() ? opportunity.GetAttributeValue<string>("customerneed") : entity.CustomerNeed;
                    opportunity["proposedsolution"] = entity.ProposedSolution.IsNullOrWhiteSpace() ? opportunity.GetAttributeValue<string>("proposedsolution") : entity.ProposedSolution;
                    opportunity["currentsituation"] = entity.CurrentSituation.IsNullOrWhiteSpace() ? opportunity.GetAttributeValue<string>("currentsituation") : entity.CurrentSituation;
                    service.Update(opportunity);
                    
                    switch (entity.CrmAnnotationEntity.AnnotationType)
                    {
                        case Base.AnnotationType.TXT:
                            CrmAnnotationEntity crmTxtAnnotationEntity = entity.CrmAnnotationEntity;
                            if (crmTxtAnnotationEntity != null && crmTxtAnnotationEntity.NoteText != string.Empty) 
                            {
                                Guid id = opportunity.Id;
                                var annotationTxtEntity = (new CrmTxtAnnotationService()).ConstructAnnotation(id, "opportunity", crmTxtAnnotationEntity);
                                if (annotationTxtEntity != null)
                                    service.Create(annotationTxtEntity);
                            }
                            break;
                    }

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
                    Message = "Update failed! The detail is: " + ex.Message
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