using DealGeinieCrmService.Base;
using System.IdentityModel.Protocols.WSTrust;
using System.Runtime.Serialization;
using Status = DealGeinieCrmService.Base.Status;

namespace DealGeinieCrmService.Models
{
    /// <summary>
    /// Represents a CRM opportunity entity.
    /// Willie Yao - 02/20/2025
    /// </summary>
    public class CrmOpportunityEntity
    {
        /// <summary>
        /// Map with opportunity id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Map with opportunity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Map with opportunity account name
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// hms_systemconstruction
        /// </summary>
        public string SystemConstruction { get; set; }

        /// <summary>
        /// hms_note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// hms_projectbudget
        /// </summary>
        public Status ProjectBudget { get; set; }

        /// <summary>
        /// hms_approveproject
        /// </summary>
        public Status ApproveProject {  get; set; }

        /// <summary>
        /// hms_communication
        /// </summary>
        public CommunicationType CommunicationType { get; set; }

        /// <summary>
        /// currentsituation
        /// </summary>
        public string CurrentSituation { get; set; }

        /// <summary>
        /// customerneed
        /// </summary>
        public string CustomerNeed {  get; set; }

        /// <summary>
        /// proposedsolution
        /// </summary>
        public string ProposedSolution { get; set; }
        
        /// <summary>
        /// CRM Annotation
        /// </summary>
        public CrmAnnotationEntity CrmAnnotationEntity { get; set; }
    }
}
