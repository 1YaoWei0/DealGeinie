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
        public string Id { get; set; }

        public string Name { get; set; }

        public string AccountName { get; set; }

        public string SystemConstruction { get; set; }

        public string Note { get; set; }

        public Status ProjectBudget { get; set; }

        public Status ApproveProject {  get; set; }

        public CommunicationType CommunicationType { get; set; }        

        public string CurrentSituation { get; set; }

        public string CustomerNeed {  get; set; }

        public string ProposedSolution { get; set; }
    }
}
