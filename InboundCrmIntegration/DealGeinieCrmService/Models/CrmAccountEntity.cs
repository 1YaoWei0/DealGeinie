using System;
using System.Runtime.Serialization;

namespace DealGeinieCrmService.Models
{
    public class CrmAccountEntity : ICrmEntity
    {
        public string name { get; set; }

        public string telephone1 { get; set; }

        public DateTime createdon { get; set; }

        public CrmAccountEntity() { }

        public CrmAccountEntity(string name, string telephone1, DateTime createdon)
        {
            this.name = name;
            this.telephone1 = telephone1;
            this.createdon = createdon;
        }
    }
}
