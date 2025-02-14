using System;
using System.Runtime.Serialization;

namespace DealGeinieCrmService.Models
{
    public class CrmAccount
    {
        public string name { get; set; }

        public string telephone1 { get; set; }

        public DateTime createdon { get; set; }

        public CrmAccount() { }
    }
}
