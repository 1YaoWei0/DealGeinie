using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InboundCrmIntegration
{
    [DataContract]
    public class CrmAccount : ICrmEntity
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string telephone1 { get; set; }

        [DataMember]
        public DateTime createdon { get; set; }

        public CrmAccount(string _name, string _telephone1, DateTime _createdon)
        {
            name = _name;
            telephone1 = _telephone1;
            createdon = _createdon;
        }
    }
}
